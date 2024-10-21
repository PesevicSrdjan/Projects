
using UserLib;
using OpenSSLLib;
using DatabaseLib;
using CryptoAlgLib;

namespace UserAuthLib
{
    public class UserAuth
    {
        public static void RegisterInfo(OpenSSL openSSL,string path, string pathForDatabase, string pathCAEnvironment, Database database, Dictionary<string, Tuple<string, string>> myDictionary, string databasePass)
        {

            Console.Clear();
            Console.WriteLine("REGISTRATION");
            Console.WriteLine("------------");

            //Unošenje podataka za korisnika.
            User user = new User(myDictionary);

            //Putanja namijenjena za korisnika.
            string pathOfUser = $"{path}\\{user.Username}";

            try
            {
                //Kreira se direktorijum za korisnika.
                Directory.CreateDirectory(pathOfUser);
                //Generisanje para ključeva.
                openSSL.GenerateKeyPair(pathOfUser, user);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem sa kreiranjem direktorijuma!");
            }

            //Generisanje otiska lozinke.
            string hash = openSSL.GeneratePasswordHash(user.Password);
            //Exportuje se javni ključ korisnika.
            string publicKey = openSSL.ExportPublicKey(pathOfUser, user);
            //Korisničko ime, hash lozinke i javni ključ se postavljaju u kolekciju.
            myDictionary.Add(user.Username, Tuple.Create(hash, publicKey));
            //Vršimo serijalizaciju kolekcije u datoteku.
            database.SerializeDictionary(myDictionary);
            //Kriptujemo datoteku simetričnim algoritmom.
            openSSL.DatabaseEncrypt(pathForDatabase, path, databasePass);
            //Vršimo brisanje neenkriptovane datoteke, a kriptovanoj mijenjamo naziv u naziv koji je imala neenkriptovana datoteka.
            string pathToEncryptedDtbs = Path.Combine(path, "database2.txt");
            DeleteFile(pathForDatabase);
            RenameFile(pathToEncryptedDtbs, pathForDatabase);

            string requestPath = Path.Combine(pathCAEnvironment, "requests");
            string certsPath = Path.Combine(pathCAEnvironment, "certs");
            //Generisanje zahtjeva za certifikat.
            openSSL.CertificateRequest(pathOfUser, requestPath, user, pathCAEnvironment);

            //Console.WriteLine(requestPath);
            //Console.WriteLine(certsPath);
            //Console.WriteLine(pathCAEnvironment);

            //Potpisivanje certifikata.
            openSSL.SignatureCertificate(user);

            //Exportovanje certifikata i ključa korisnika u pkcs12 datoteku.
            openSSL.ExportToPKCS12(pathOfUser, pathOfUser, certsPath, pathCAEnvironment, user);
            //Brisanje iz direktorijuma korisnika njegov privatni ključ.
            string pathToUsrPrivKey = Path.Combine(pathOfUser, "keypair.key");
            DeleteFile(pathToUsrPrivKey);

            string pathOfUsrCertAndKey = $"{pathOfUser}\\{user.Username}.p12";
            string crlPath = Path.Combine(pathCAEnvironment, "crl");
            string configPath = Path.Combine(pathCAEnvironment, "openssl.cnf");
            string privatePath = Path.Combine(pathCAEnvironment, "private");
            openSSL.GenerateCRLList(crlPath, configPath, privatePath);
            Console.WriteLine("Putanja do generisanog certifikata i ključa je:" + pathOfUsrCertAndKey);
        }

        public static void LoginInfo(OpenSSL openSSL,string path, Dictionary<string, Tuple<string, string>> myDictionary)
        {
            Console.Clear();
            Console.WriteLine("PRIJAVA");
            Console.WriteLine("---------------");
            Console.WriteLine("Sve putanje do certifikata su oblika:C:\\Users\\DT User\\Desktop\\Cryptography Simulator\\Korisnicko_Ime\\Korisnicko_Ime.p12");
            Console.WriteLine("Unesite putanju do certifikata: ");
            string pathToUsrCertAndKey = Console.ReadLine();
            if (File.Exists(pathToUsrCertAndKey))
            {
                string nameOfFolder = Path.GetFileNameWithoutExtension(pathToUsrCertAndKey);
                string pathOfUser = Path.Combine(path, nameOfFolder);
                //Console.WriteLine(pathOfUser);
                string pathToPriv = Path.Combine(path, "CA Environment", "private");

                //izdvojiti certifikat iz p12 datoteke, pa iz certifikata izdvojiti javni kljuc, i uporediti sa onim u Dictionary.
                Console.WriteLine("Unesite lozinku od PKCS12 datoteke:");
                string pkcsPassword = Console.ReadLine();


                //ExportCertFromPKCS12(pathToUsrCertAndKey, pathOfUser, pkcsPassword);
                string pathToUserCert = Path.Combine(pathOfUser, "client.crt");

                openSSL.ExportCertFromPKCS12(pathToUsrCertAndKey, pathOfUser, pkcsPassword);
                string publicKey = openSSL.ExportPublicKeyFromCert(pathOfUser);
                DeleteFile(pathToUserCert);

                if (!String.IsNullOrEmpty(publicKey))
                {
                    Console.Clear();
                    string? usernameFromDtbs = null;
                    string? passwordFromDtbs = null;
                    foreach (var pair in myDictionary)
                    {
                        if (pair.Value.Item2.Equals(publicKey))
                        {
                            usernameFromDtbs = pair.Key;
                            passwordFromDtbs = pair.Value.Item1;
                        }
                    }

                    //Console.WriteLine(usernameFromDtbs);
                    //Console.WriteLine(passwordFromDtbs);
                    Console.WriteLine("Unesite korisničko ime:");
                    #pragma warning disable CS8600
                    string username = Console.ReadLine();
                    Console.WriteLine("Unesite lozinku:");
                    string password = Console.ReadLine();
                    #pragma warning disable CS8602
                    if (username.Equals(usernameFromDtbs) && openSSL.VerifyPassword(passwordFromDtbs, password))
                    {
                        Console.Clear();
                        string fileForDelete = $"{pathToPriv}\\{username}.key";

                        openSSL.ExportPrivKeyFromPKCS12(pathToUsrCertAndKey, pathToPriv, username, pkcsPassword);


                        //Potrebno je napraviti datoteku sa kljucem kojim će biti kriptovana simulation datoteka nekim simetricnim algoritmom, a kljuc sa datotekom kriptovati javnim kljucem korisnika.
                        string pathToSimulPass = Path.Combine(pathOfUser, "simulPass.txt");
                        string simulPassEncryptPath = Path.Combine(pathOfUser, "SimulPassEncrypted.txt");
                        string hashOfFile = Path.Combine(pathOfUser, "FileHash.txt");
                        if (!File.Exists(simulPassEncryptPath))
                        {
                            // Ako datoteka ne postoji, kreira je i upiši tekst
                            if (!File.Exists(pathToSimulPass))
                            {
                                try
                                {
                                    using (StreamWriter writer = File.CreateText(pathToSimulPass))
                                    {
                                        writer.WriteLine(password);
                                    }

                                    //Console.WriteLine("Datoteka je uspješno kreirana i tekst je upisan.");
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Došlo je do greške prilikom kreiranja datoteke: " + ex.Message);
                                }
                            }

                            string publicKeyPath = Path.Combine(pathOfUser, "public.key");

                            try
                            {
                                using (StreamWriter writer = File.CreateText(publicKeyPath))
                                {
                                    writer.WriteLine(publicKey);
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Došlo je do greške prilikom kreiranja datoteke: " + ex.Message);
                            }

                            openSSL.SimulPassEncrypt(pathToSimulPass, simulPassEncryptPath, publicKeyPath);
                            DeleteFile(publicKeyPath);
                            DeleteFile(pathToSimulPass);

                        }
                        //Dekriptovanje SimulEncryptedPass.txt korištenjem privatnog ključa korisnika.

                        string pass = openSSL.SimulPassDecrypt(simulPassEncryptPath, fileForDelete, password);
                        //Console.WriteLine(pass);

                        string simulatFilePath = Path.Combine(pathOfUser, "simulation.txt");
                        string signaturePath = Path.Combine(pathOfUser, "potpis.txt");

                        string pubKeyPath = Path.Combine(pathOfUser, "public.key");

                        try
                        {
                            using (StreamWriter writer = File.CreateText(pubKeyPath))
                            {
                                writer.WriteLine(publicKey);
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Došlo je do greške prilikom kreiranja datoteke: " + ex.Message);
                        }
                        if (File.Exists(simulatFilePath) && File.Exists(signaturePath))
                        {
                            string validate = openSSL.Validate(pubKeyPath, signaturePath, simulatFilePath);
                            if (!validate.Contains("OK"))
                            {
                                Console.WriteLine("PAŽNJA:Došlo je do neovlaštene promjene!");
                                Console.WriteLine("Sačekajte 5 sekundi!");
                                Thread.Sleep(5000);
                            }

                        }

                        DeleteFile(pubKeyPath);

                        bool exit = false;

                        do
                        {
                            Console.Clear();
                            Console.WriteLine("Dobro došli korisniče " + username);
                            Console.WriteLine("ALGORITMI");
                            Console.WriteLine("---------------");
                            Console.WriteLine("1.Rail fence");
                            Console.WriteLine("2.Myszkowski");
                            Console.WriteLine("3.Playfair");
                            Console.WriteLine("4.Prikaz datoteke");
                            Console.WriteLine("5.Exit");
                            Console.WriteLine("Unesite algoritam: ");
                            string inputAlg = Console.ReadLine();
                            string filePath = Path.Combine(pathOfUser, "simulation.txt");
                            string filePathEnc = Path.Combine(pathOfUser, "simulation2.txt");
                            string decryptPath = Path.Combine(pathOfUser, "decrypted.txt");
                            string text = null;

                            string data = openSSL.DecryptSimulation(filePath, pass);
                            switch (inputAlg)
                            {
                                case "1":
                                    Console.Clear();
                                    Console.WriteLine("Unesite tekst koji želite da enkriptujete:");
                                    text = Console.ReadLine();
                                    Console.WriteLine("Unesite koliko zelite kolosjeka:");
                                    string val = Console.ReadLine();
                                    int number = Convert.ToInt32(val);
                                    if (text.Length <= 100 && !String.IsNullOrEmpty(text))
                                    {
                                        if (File.Exists(filePath))
                                        {
                                            openSSL.DecryptSimulaToFile(filePath, decryptPath, pass);
                                            DeleteFile(filePath);
                                            RenameFile(decryptPath, filePath);
                                        }
                                        string encryptedText = RailFence.EncryptRailFence(text, number);
                                        Console.WriteLine(encryptedText);

                                        StreamWriter writer = new StreamWriter(filePath, true);
                                        writer.WriteLine($"\"{text}\"|RAIL FENCE|\"{number}\"|\"{encryptedText}\"");
                                        writer.Close();
                                        openSSL.EncryptSimulation(filePath, filePathEnc, pass);
                                        DeleteFile(filePath);
                                        RenameFile(filePathEnc, filePath);

                                    }
                                    else
                                    {
                                        Console.WriteLine("Broj karaktera premašuje 100!");

                                    }
                                    break;

                                case "2":
                                    Console.Clear();
                                    Console.WriteLine("Unesite tekst koji želite da enkriptujete:");
                                    text = Console.ReadLine();
                                    Console.WriteLine("Unesite ključ za enkripciju:");
                                    string key = Console.ReadLine();
                                    if (text.Length <= 100 && !String.IsNullOrEmpty(text))
                                    {
                                        if (File.Exists(filePath))
                                        {
                                            openSSL.DecryptSimulaToFile(filePath, decryptPath, pass);
                                            DeleteFile(filePath);
                                            RenameFile(decryptPath, filePath);
                                        }

                                        Myszkowski myszkowski = new Myszkowski();
                                        string encryptedText = myszkowski.MyszkEncrypt(key, text);
                                        Console.WriteLine(encryptedText);
                                        StreamWriter writer = new StreamWriter(filePath, true);
                                        writer.WriteLine($"\"{text}\"|MYSZKOWSKI|\"{key}\"|\"{encryptedText}\"");
                                        writer.Close();
                                        openSSL.EncryptSimulation(filePath, filePathEnc, pass);
                                        DeleteFile(filePath);
                                        RenameFile(filePathEnc, filePath);

                                    }
                                    else
                                    {
                                        Console.WriteLine("Broj karaktera premašuje 100!");
                                    }

                                    break;

                                case "3":
                                    Console.Clear();
                                    Console.WriteLine("Unesite tekst koji želite da enkriptujete:");
                                    text = Console.ReadLine();
                                    Console.WriteLine("Unesite ključ za enkripciju:");
                                    string keyPF = Console.ReadLine();
                                    if (text.Length <= 100 && !String.IsNullOrEmpty(text))
                                    {
                                        if (File.Exists(filePath))
                                        {
                                            openSSL.DecryptSimulaToFile(filePath, decryptPath, pass);
                                            DeleteFile(filePath);
                                            RenameFile(decryptPath, filePath);
                                        }
                                        PlayFair playFair = new PlayFair();
                                        string encryptedPF = playFair.Encipher(text, keyPF);
                                        Console.WriteLine(encryptedPF);
                                        StreamWriter writer = new StreamWriter(filePath, true);
                                        writer.WriteLine($"\"{text}\"|PLAYFAIR|\"{keyPF}\"|\"{encryptedPF}\"");
                                        writer.Close();
                                        openSSL.EncryptSimulation(filePath, filePathEnc, pass);
                                        DeleteFile(filePath);
                                        RenameFile(filePathEnc, filePath);

                                    }
                                    else
                                    {
                                        Console.WriteLine("Broj karaktera premašuje 100!");
                                    }
                                    break;
                                case "4":
                                    Console.Clear();
                                    if (!String.IsNullOrEmpty(data))
                                    {
                                        Console.WriteLine(data);

                                    }
                                    else
                                    {
                                        Console.WriteLine("Prazna datoteka!");

                                    }
                                    break;
                                case "5":
                                    exit = true;
                                    break;

                                default:
                                    Console.WriteLine("Nevažeći unos!");
                                    break;
                            }
                            if (exit)
                            {
                                break; // Izađi iz petlje ako je korisnik odabrao Exit.
                            }

                            Console.WriteLine("Unesite bilo koju tipku za povratak na početni ekran.");
                        } while (Console.ReadKey(true).Key != ConsoleKey.Escape);

                        if (File.Exists(simulatFilePath))
                        {
                            //HashOfFile(simulatFilePath, hashOfFile);
                            openSSL.CalculateSignature(fileForDelete, signaturePath, simulatFilePath, password);

                        }

                        DeleteFile(fileForDelete);
                    }
                    else
                    {
                        Console.WriteLine("Netačni podaci!");
                    }

                }
                else
                {
                    Console.WriteLine("Netačna lozinka od PKCS12 datoteke!");
                }
            }
            else
            {
                Console.WriteLine("Navedena putanja ne postoji!");

            }
        }
        private static void DeleteFile(string pathToFile)
        {
            if (File.Exists(pathToFile))
            {
                File.Delete(pathToFile);

            }
            else
            {
                Console.WriteLine("Greška!");
            }
        }
        private static void RenameFile(string pathToFile, string pathForDatabase)
        {
            if (File.Exists(pathToFile))
            {
                File.Move(pathToFile, pathForDatabase);
            }
            else
            {
                Console.WriteLine("Greška pri preimenovanju!");
            }
        }

    }
}