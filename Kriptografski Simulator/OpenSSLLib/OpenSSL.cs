using System.Diagnostics;
using System.Transactions;
using UserLib;

namespace OpenSSLLib
{
    public class OpenSSL
    {
        public OpenSSL(){}

        public void OpenSSLCommand(string command)
        {
            Process opensslProcess = new Process();
            opensslProcess.StartInfo.FileName = @"C:\Program Files\OpenSSL-Win64\bin\openssl.exe";
            opensslProcess.StartInfo.Arguments = command;
            opensslProcess.StartInfo.RedirectStandardOutput = true;
            opensslProcess.StartInfo.RedirectStandardError = true;
            opensslProcess.StartInfo.UseShellExecute = false;
            opensslProcess.StartInfo.CreateNoWindow = true;
            opensslProcess.Start();

            // Čitanje izlaza i grešaka
            string output = opensslProcess.StandardOutput.ReadToEnd();
            string errors = opensslProcess.StandardError.ReadToEnd();

            // Čekanje da se proces završi
            opensslProcess.WaitForExit();

            // Ispisivanje izlaza i grešaka u konzolu
            //Console.WriteLine(output);
            //Console.WriteLine(errors);
        }
        public void OpenSSLCommandSignature(string command)
        {
            Process opensslProcess = new Process();
            opensslProcess.StartInfo.FileName = @"C:\Program Files\OpenSSL-Win64\bin\openssl.exe";
            opensslProcess.StartInfo.Arguments = command;
            opensslProcess.StartInfo.RedirectStandardOutput = true;
            opensslProcess.StartInfo.RedirectStandardError = true;
            opensslProcess.StartInfo.UseShellExecute = false;
            opensslProcess.StartInfo.CreateNoWindow = true;
            opensslProcess.StartInfo.WorkingDirectory = @"C:\Users\DT User\Desktop\Cryptography Simulator\CA Environment";
            opensslProcess.Start();

            // Čitanje izlaza i grešaka
            string output = opensslProcess.StandardOutput.ReadToEnd();
            string errors = opensslProcess.StandardError.ReadToEnd();

            // Čekanje da se proces završi
            opensslProcess.WaitForExit();

            // Ispisivanje izlaza i grešaka u konzolu
            //Console.WriteLine(output);
            //Console.WriteLine(errors);


        }
        public void GenerateKeyPair(string outputPath, User user)
        {
            string opensslCommand = $"genrsa -passout pass:{user.Password} -out \"{outputPath}\\keypair.key\" -aes-128-cbc 2048";
            OpenSSLCommand(opensslCommand);
        }
        public string OpenSSLCommandStringReturn(string command)
        {
            Process opensslProcess = new Process();
            opensslProcess.StartInfo.FileName = @"C:\Program Files\OpenSSL-Win64\bin\openssl.exe";
            opensslProcess.StartInfo.Arguments = command;
            opensslProcess.StartInfo.RedirectStandardOutput = true;
            opensslProcess.StartInfo.RedirectStandardError = true;
            opensslProcess.StartInfo.UseShellExecute = false;
            opensslProcess.StartInfo.CreateNoWindow = true;
            opensslProcess.Start();

            // Čitanje izlaza i grešaka
            string output = opensslProcess.StandardOutput.ReadToEnd();
            string errors = opensslProcess.StandardError.ReadToEnd();

            // Čekanje da se proces završi
            opensslProcess.WaitForExit();

            // Ispisivanje izlaza i grešaka u konzolu
            //Console.WriteLine(output);
            //Console.WriteLine(errors);
            return output;
        }
        public string GeneratePasswordHash(string password)
        {
            string opensslCommand = $"passwd -5 {password}";
            string hash = OpenSSLCommandStringReturn(opensslCommand);
            return hash;
        }
        public string GeneratePasswordHashWithSalt(string password, string salt)
        {
            string opensslCommand = $"passwd -5 -salt {salt} {password}";
            string hash = OpenSSLCommandStringReturn(opensslCommand);
            return hash;
        }
        public bool VerifyPassword(string hash, string password)
        {
            string[] parts = hash.Split('$');

            string algorithm = parts[1];
            string salt = parts[2];
            string other = parts[3];
            //Console.WriteLine(algorithm);
            //Console.WriteLine(salt);
            //Console.WriteLine(other);
            string verify = GeneratePasswordHashWithSalt(password, salt);
            //Console.WriteLine(verify);
            if (verify.Equals(hash))
                return true;
            return false;

        }
        public string ExportPublicKey(string privateKeyPath, User user)
        {
            string opensslCommand = $"rsa -in \"{privateKeyPath}\\keypair.key\" -passin pass:{user.Password} -pubout";
            string publicKey = OpenSSLCommandStringReturn(opensslCommand);
            return publicKey;
        }
        public void DatabaseEncrypt(string databasePath, string storePath, string password)
        {
            string opensslCommand = $"aes-128-cbc -in \"{databasePath}\" -out \"{storePath}\\database2.txt\" -k \"{password}\"";
            OpenSSLCommand(opensslCommand);

        }

        public void SimulPassEncrypt(string simulPassPath, string storePath, string publicKeyPath)
        {
            string opensslCommand = $"pkeyutl -encrypt -in \"{simulPassPath}\" -out \"{storePath}\" -inkey \"{publicKeyPath}\" -pubin";
            OpenSSLCommand(opensslCommand);

        }

        public string SimulPassDecrypt(string simulPassEncryPath, string privKeyUser, string password)
        {
            string opensslCommand = $"pkeyutl -decrypt -in \"{simulPassEncryPath}\" -inkey \"{privKeyUser}\" -passin pass:{password}";
            string pass = OpenSSLCommandStringReturn(opensslCommand);
            return pass;
        }

        public void HashOfFile(string filePath, string storePath)
        {
            string opensslCommand = $"dgst -sha1 -out \"{storePath}\" \"{filePath}\"";
            OpenSSLCommand(opensslCommand);

        }

        public void CalculateSignature(string privUserKey, string storePath, string filePath, string password)
        {
            string opensslCommand = $"dgst -sha1 -sign \"{privUserKey}\" -keyform PEM -out \"{storePath}\" -passin pass:{password} \"{filePath}\"";
            OpenSSLCommand(opensslCommand);

        }
        public string Validate(string publicKeyPath, string signaturePath, string filePath)
        {
            string opensslCommand = $"dgst -sha1 -verify \"{publicKeyPath}\" -signature \"{signaturePath}\" \"{filePath}\"";
            string validate = OpenSSLCommandStringReturn(opensslCommand);
            return validate;
        }

        public string DatabaseDecrypt(string path, string password)
        {
            string opensslCommand = $"aes-128-cbc -d -in \"{path}\\database.txt\" -k \"{password}\"";
            string data = OpenSSLCommandStringReturn(opensslCommand);
            return data;
        }
        public void CertificateRequest(string privateKeyPath, string storePath, User user, string configurePath)
        {
            string opensslCommand = $"req -new -out \"{storePath}\\{user.Username}.crs\" -key \"{privateKeyPath}\\keypair.key\" -config \"{configurePath}\\openssl.cnf\" -passin pass:{user.Password} -subj \"/C={user.Country}/ST={user.StateOrProvince}/L={user.Locality}/O={user.Organization}/OU={user.OrganizationUnit}/CN={user.CommonName}/emailAddress={user.EmailAddress}\"";
            OpenSSLCommand(opensslCommand);
        }
        public void SignatureCertificate(User user)
        {
            string opensslCommand = $"ca -in \"requests\\{user.Username}.crs\" -out \"certs\\{user.Username}.crt\" -config openssl.cnf -passin pass:sigurnost -batch";
            OpenSSLCommandSignature(opensslCommand);
        }
        public string DatabasePassDecrypt(string databasePassPath, string CAPrivatePath)
        {
            string opensslCommand = $"pkeyutl -decrypt -in \"{databasePassPath}\" -inkey \"{CAPrivatePath}\\CAKey4096.key\" -passin pass:sigurnost";
            string decryptedPass = OpenSSLCommandStringReturn(opensslCommand);
            return decryptedPass;

        }

        public void ExportToPKCS12(string storePath, string userPrvKPath, string certPath, string caCertPath, User user)
        {
            string opensslCommand = $"pkcs12 -export -out \"{storePath}\\{user.Username}.p12\" -inkey \"{userPrvKPath}\\keypair.key\" -in \"{certPath}\\{user.Username}.crt\" -certfile \"{caCertPath}\\rootca.pem\" -passin pass:{user.Password} -passout pass:{user.Password}";
            OpenSSLCommand(opensslCommand);

        }

        public void EncryptSimulation(string simulatPath, string storePath, string password)
        {
            string opensslCommand = $"aes-128-cbc -in \"{simulatPath}\" -out \"{storePath}\" -k \"{password}\"";
            OpenSSLCommand(opensslCommand);
        }

        public string DecryptSimulation(string simulatPath, string password)
        {
            string opensslCommand = $"aes-128-cbc -d -in \"{simulatPath}\" -k \"{password}\"";
            string data = OpenSSLCommandStringReturn(opensslCommand);
            return data;

        }
        public void DecryptSimulaToFile(string simulatPath, string storePath, string password)
        {
            string opensslCommand = $"aes-128-cbc -d -in \"{simulatPath}\" -out \"{storePath}\" -k \"{password}\"";
            OpenSSLCommand(opensslCommand);
        }

        public void GenerateCRLList(string storePath, string configPath, string caPrivKeyPath)
        {
            string opensslCommand = $"ca -gencrl -out \"{storePath}\\lista.pem\" -config \"{configPath}\" -keyfile \"{caPrivKeyPath}\\CAKey4096.key\" -passin pass:sigurnost";
            OpenSSLCommandSignature(opensslCommand);
        }

        public void ExportPrivKeyFromPKCS12(string pkcsPath, string storePrvKey, string username, string password)
        {
            string opensslCommand = $"pkcs12 -in \"{pkcsPath}\" -nocerts -out \"{storePrvKey}\\{username}.key\" -passin pass:{password} -passout pass:{password}";
            OpenSSLCommand(opensslCommand);
        }

        public void ExportCertFromPKCS12(string pkcsPath, string storeCert, string password)
        {
            string opensslCommand = $"pkcs12 -in \"{pkcsPath}\" -nokeys -clcerts -out \"{storeCert}\\client.crt\" -passin pass:{password}";
            OpenSSLCommand(opensslCommand);
        }
        public string ExportPublicKeyFromCert(string certPath)
        {
            string opensslCommand = $"x509 -in \"{certPath}\\client.crt\" -pubkey -noout";
            string publicKey = OpenSSLCommandStringReturn(opensslCommand);
            return publicKey;
        }
    }
}