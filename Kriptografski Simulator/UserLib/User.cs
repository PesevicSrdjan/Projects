namespace UserLib
{
    public class User
    {

        private string username, password, country, stateOrProvince, locality, organization,organizationUnit, commonName, emailAddress;

        public User(Dictionary<string, Tuple<string, string>> existingUsers)
        {
            EnterUserData(existingUsers);
        }
        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public string Country { get => country; set => country = value; }
        public string StateOrProvince { get => stateOrProvince; set => stateOrProvince = value; }
        public string Locality { get => locality; set => locality = value; }
        public string Organization { get => organization; set => organization = value; }
        public string OrganizationUnit { get => organizationUnit; set => organizationUnit = value; }
        public string CommonName { get => commonName; set => commonName = value; }
        public string EmailAddress { get => emailAddress; set => emailAddress = value; }

        public void EnterUserData(Dictionary<string,Tuple<string,string>> existingUsers)
        {

            Console.Write("Username:");
            do
            {
                this.username = ReadNonEmptyInput();

                if (existingUsers.ContainsKey(this.Username))
                {
                    Console.WriteLine("Greška! Korisničko ime već postoji. Unesite ponovo korisničko ime.");
                }

            } while (existingUsers.ContainsKey(this.Username));

            Console.Write("Password:");
            this.password = ReadNonEmptyInput();
            Console.Write("Country:");
            this.country = ReadNonEmptyInput();
            Console.Write("State or Province:");
            this.stateOrProvince = ReadNonEmptyInput();
            Console.Write("Locality:");
            this.locality = ReadNonEmptyInput();
            Console.Write("Organization:");
            this.organization = ReadNonEmptyInput();
            Console.Write("Organization Unit:");
            this.organizationUnit = ReadNonEmptyInput();
            Console.Write("CommonName:");
            this.commonName = ReadNonEmptyInput();
            Console.Write("EmailAddress:");
            this.emailAddress = ReadNonEmptyInput();
        }

        private string ReadNonEmptyInput()
        {
            string? input;
            do
            {
                input = Console.ReadLine();
                if(string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Greška!Unos ne može biti prazan! Pokušajte ponovo.");
                }

            }while (string.IsNullOrEmpty(input));

            return input;
        }
    }
}