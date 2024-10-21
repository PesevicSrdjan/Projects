using UserLib;
using DatabaseLib;
using System.Diagnostics;
using CryptoAlgLib;
using Microsoft.VisualBasic;
using UserAuthLib;
using OpenSSLLib;

bool exitRequested = false;
string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
string path = Path.Combine(desktopPath, "Cryptography Simulator");

//Potrebne putanje:
string pathForDatabase = Path.Combine(path, "database.txt");
string pathCAEnvironment = Path.Combine(path, "CA Environment");
string pathPrivate = Path.Combine(pathCAEnvironment, "private");
string databasePassPath = Path.Combine(path, "databasepass.txt");

Database database = new Database(pathForDatabase);
Dictionary<string, Tuple<string, string>> myDictionary = new Dictionary<string, Tuple<string, string>>();

OpenSSL openSSL = new OpenSSL();

string databasePass = openSSL.DatabasePassDecrypt(databasePassPath, pathPrivate);


if(File.Exists(pathForDatabase))
{
    string data = openSSL.DatabaseDecrypt(path, databasePass);
    if(!String.IsNullOrEmpty(data))
    {
        myDictionary = database.DeserializeDictionary(data);
    }
}
do
{
    Console.Clear();
    Console.WriteLine("DOBRO DOŠLI U KRIPTOGRAFSKI SIMULATOR");
    Console.WriteLine("---------------------------------------------");
    Console.WriteLine("1.Registracija");
    Console.WriteLine("2.Prijava");
    Console.WriteLine("3.Izlaz");
    Console.Write("Unos: ");

    var input = Console.ReadLine();

    switch (input)
    {
        case "1":
            UserAuth.RegisterInfo(openSSL,path,pathForDatabase,pathCAEnvironment, database, myDictionary,databasePass);
            break;
        case "2":
            UserAuth.LoginInfo(openSSL,path, myDictionary);
            break;
        case "3":
            exitRequested = true;
            break;
        default:
            Console.WriteLine("Nevažeći unos.");
            break;
    }
    if(exitRequested)
    {
        break;
    }
    Console.WriteLine("Unesite bilo koju tipku za povratak na početni ekran.");
} while (Console.ReadKey(true).Key != ConsoleKey.Escape);


