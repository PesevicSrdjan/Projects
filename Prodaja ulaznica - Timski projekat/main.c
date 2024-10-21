#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <time.h>

static int credits = 0;

struct tm get_today() {
    time_t t = time(NULL);
    return *localtime(&t);
}

typedef struct date {
    int day, month, year;
} Date;

typedef struct Event {
    char name[30];
    char descr[300];
    char place[20];
    char date[20];
    char time[10];
    char buyOnName[5];
    float price;
} Event;

void clrscr() {
    system("@cls||clear");
}

void function_n_login_admin(char *username,char *real_name)
{
    int temporary;
    char *ogranicenje_username;
    char ogran_user[100];
    char *ogranicenje_broj;
    char ogran_broj[100];
    FILE *fp_ogranicenje=fopen("n_login.txt","r+");
    char line_ogranicenje[100];
    while(fgets(line_ogranicenje,sizeof(line_ogranicenje),fp_ogranicenje)!=NULL)
    {
        line_ogranicenje[strcspn(line_ogranicenje,"\n")]=0;
        if(strstr(line_ogranicenje,username)!=NULL)
        {
            //printf("%s",line_ogranicenje);
            ogranicenje_username=strtok(line_ogranicenje," ");
            ogranicenje_broj=strtok(NULL," ");
            strcpy(ogran_user,ogranicenje_username);
            strcpy(ogran_broj,ogranicenje_broj);

        }

    }fclose(fp_ogranicenje);

    sscanf(ogran_broj,"%d",&temporary);
    if(temporary==5)
    {
        int z;
        char* ogranicenje_username2;
        char* ogranicenje_broj2;
        function_repeal(username);
        FILE *fp_ogranicenje2=fopen("n_login.txt","r+");
        FILE *fp_ogranicenje3=fopen("temporary.txt","a");
        char line_ogranicenje2[100];
        while(fgets(line_ogranicenje2,sizeof(line_ogranicenje2),fp_ogranicenje2)!=NULL)
        {
            line_ogranicenje2[strcspn(line_ogranicenje2,"\n")]=0;
            if(strstr(line_ogranicenje2,username)!=NULL)
            {
                ogranicenje_username2=strtok(line_ogranicenje2," ");
                ogranicenje_broj2=strtok(NULL," ");
                sscanf(ogranicenje_broj2,"%d",&z);
                fprintf(fp_ogranicenje3,"%s %d\n",ogranicenje_username2,0);

            }
            else
            {
                fputs(line_ogranicenje2,fp_ogranicenje3);
                fputs("\n",fp_ogranicenje3);
            }

        }
        fclose(fp_ogranicenje2);
        fclose(fp_ogranicenje3);
        remove("n_login.txt");
        rename("temporary.txt","n_login.txt");
        clrscr();
        main();
    }
    else
    {
        clrscr();
        int y;
        char *username_from_n_login;
        char *number_of_n_login;
        FILE *fp_for_n_login=fopen("n_login.txt","r+");
        FILE *fp_temp=fopen("n_login_temp.txt","a+");
        char line_for_n_login[100];
        while(fgets(line_for_n_login,sizeof(line_for_n_login),fp_for_n_login)!=NULL)
        {
            if(strstr(line_for_n_login,username)!=NULL)
            {

                username_from_n_login=strtok(line_for_n_login," ");
                number_of_n_login=strtok(NULL," ");
                sscanf(number_of_n_login,"%d",&y);
                y++;
                fprintf(fp_temp,"%s %d\n",username_from_n_login,y);
            }
            else
            {
                fputs(line_for_n_login,fp_temp);
            }
        }

        fclose(fp_for_n_login);
        fclose(fp_temp);
        remove("n_login.txt");
        rename("n_login_temp.txt","n_login.txt");
        printf("Dobro dosli admine %s!\n\n", real_name);
        //TODO ADMINOVOG NALOGA
        //Kreiranje samo opcije kreiraj nalog klijenta kako bi imali kreirane sve naloge!
        admin_interface();
    }
}

void funtion_n_login_user(char *username,char *real_name,char *username_from_file)
{
    int temporary;
    char *ogranicenje_username;
    char ogran_user[100];
    char *ogranicenje_broj;
    char ogran_broj[100];
    FILE *fp_ogranicenje=fopen("n_login.txt","r+");
    char line_ogranicenje[100];
    while(fgets(line_ogranicenje,sizeof(line_ogranicenje),fp_ogranicenje)!=NULL)
    {
        line_ogranicenje[strcspn(line_ogranicenje,"\n")]=0;
        if(strstr(line_ogranicenje,username)!=NULL)
        {
            //printf("%s",line_ogranicenje);
            ogranicenje_username=strtok(line_ogranicenje," ");
            ogranicenje_broj=strtok(NULL," ");
            strcpy(ogran_user,ogranicenje_username);
            strcpy(ogran_broj,ogranicenje_broj);

        }

    }fclose(fp_ogranicenje);

    sscanf(ogran_broj,"%d",&temporary);
    if(temporary==5)
    {
        int z;
        char* ogranicenje_username2;
        char* ogranicenje_broj2;
        function_repeal(username);
        FILE *fp_ogranicenje2=fopen("n_login.txt","r+");
        FILE *fp_ogranicenje3=fopen("temporary.txt","a");
        char line_ogranicenje2[100];
        while(fgets(line_ogranicenje2,sizeof(line_ogranicenje2),fp_ogranicenje2)!=NULL)
        {
            line_ogranicenje2[strcspn(line_ogranicenje2,"\n")]=0;
            if(strstr(line_ogranicenje2,username)!=NULL)
            {
                ogranicenje_username2=strtok(line_ogranicenje2," ");
                ogranicenje_broj2=strtok(NULL," ");
                sscanf(ogranicenje_broj2,"%d",&z);
                fprintf(fp_ogranicenje3,"%s %d\n",ogranicenje_username2,0);

            }
            else
            {
                fputs(line_ogranicenje2,fp_ogranicenje3);
                fputs("\n",fp_ogranicenje3);
            }

        }
        fclose(fp_ogranicenje2);
        fclose(fp_ogranicenje3);
        remove("n_login.txt");
        rename("temporary.txt","n_login.txt");
        clrscr();
        main();
    }
    else
    {
        clrscr();
        int y;
        char *username_from_n_login;
        char *number_of_n_login;
        FILE *fp_for_n_login=fopen("n_login.txt","r+");
        FILE *fp_temp=fopen("n_login_temp.txt","a+");
        char line_for_n_login[100];
        while(fgets(line_for_n_login,sizeof(line_for_n_login),fp_for_n_login)!=NULL)
        {
            if(strstr(line_for_n_login,username)!=NULL)
            {

                username_from_n_login=strtok(line_for_n_login," ");
                number_of_n_login=strtok(NULL," ");
                sscanf(number_of_n_login,"%d",&y);
                y++;
                fprintf(fp_temp,"%s %d\n",username_from_n_login,y);
            }
            else
            {
                fputs(line_for_n_login,fp_temp);
            }
        }

        fclose(fp_for_n_login);
        fclose(fp_temp);
        remove("n_login.txt");
        rename("n_login_temp.txt","n_login.txt");
        printf("Dobro dosli korisnice %s!\n", real_name);
        user_interface(real_name, username_from_file);
    }

}

void function_n_login_client(char *username, char *real_name)
{
    int temporary;
    char *ogranicenje_username;
    char ogran_user[100];
    char *ogranicenje_broj;
    char ogran_broj[100];
    FILE *fp_ogranicenje=fopen("n_login.txt","r+");
    char line_ogranicenje[100];
    while(fgets(line_ogranicenje,sizeof(line_ogranicenje),fp_ogranicenje)!=NULL)
    {
        line_ogranicenje[strcspn(line_ogranicenje,"\n")]=0;
        if(strstr(line_ogranicenje,username)!=NULL)
        {
            //printf("%s",line_ogranicenje);
            ogranicenje_username=strtok(line_ogranicenje," ");
            ogranicenje_broj=strtok(NULL," ");
            strcpy(ogran_user,ogranicenje_username);
            strcpy(ogran_broj,ogranicenje_broj);

        }

    }fclose(fp_ogranicenje);

    sscanf(ogran_broj,"%d",&temporary);
    if(temporary==5)
    {
        int z;
        char* ogranicenje_username2;
        char* ogranicenje_broj2;
        function_repeal(username);
        FILE *fp_ogranicenje2=fopen("n_login.txt","r+");
        FILE *fp_ogranicenje3=fopen("temporary.txt","a");
        char line_ogranicenje2[100];
        while(fgets(line_ogranicenje2,sizeof(line_ogranicenje2),fp_ogranicenje2)!=NULL)
        {
            line_ogranicenje2[strcspn(line_ogranicenje2,"\n")]=0;
            if(strstr(line_ogranicenje2,username)!=NULL)
            {
                ogranicenje_username2=strtok(line_ogranicenje2," ");
                ogranicenje_broj2=strtok(NULL," ");
                sscanf(ogranicenje_broj2,"%d",&z);
                fprintf(fp_ogranicenje3,"%s %d\n",ogranicenje_username2,0);

            }
            else
            {
                fputs(line_ogranicenje2,fp_ogranicenje3);
                fputs("\n",fp_ogranicenje3);
            }

        }
        fclose(fp_ogranicenje2);
        fclose(fp_ogranicenje3);
        remove("n_login.txt");
        rename("temporary.txt","n_login.txt");
        clrscr();
        main();
    }
    else
    {
        clrscr();
        int y;
        char *username_from_n_login;
        char *number_of_n_login;
        FILE *fp_for_n_login=fopen("n_login.txt","r+");
        FILE *fp_temp=fopen("n_login_temp.txt","a+");
        char line_for_n_login[100];
        while(fgets(line_for_n_login,sizeof(line_for_n_login),fp_for_n_login)!=NULL)
        {
            if(strstr(line_for_n_login,username)!=NULL)
            {

                username_from_n_login=strtok(line_for_n_login," ");
                number_of_n_login=strtok(NULL," ");
                sscanf(number_of_n_login,"%d",&y);
                y++;
                fprintf(fp_temp,"%s %d\n",username_from_n_login,y);
            }
            else
            {
                fputs(line_for_n_login,fp_temp);
            }
        }

        fclose(fp_for_n_login);
        fclose(fp_temp);
        remove("n_login.txt");
        rename("n_login_temp.txt","n_login.txt");
        printf("Dobro dosli klijentu %s\n", real_name);
        client_interface(real_name, username);
    }
}

void Exit()
{
    printf("Ukucajte  'Exit'  za povratak na pocetnu stranicu!\n\n");
    char exit[10];
    do{
        printf("Your choice: ");
        scanf("%s", exit);
    } while(strcmp(exit,"Exit") != 0);

    clrscr();
    main();
}

void function_repeal(char *username)
{
    char new_pw[50];
    FILE *fp5=fopen("users.txt","r");
    FILE *fp4=fopen("privremeni.txt","a+");
    char line_repeal[100];
    printf("Vasem nalogu je ponistena sifra. Morate da ukucate novu!\n\n");
    printf("Nova sifra: ");
    scanf("%s",new_pw);
    while(fgets(line_repeal,sizeof(line_repeal),fp5)!=NULL)
    {
        if(strstr(line_repeal,username)!=NULL)
        {

            char *parts;
            char parts1[100];
            char *user;
            char user1[100];
            char *pw;
            char pw1[100];
            char *name;
            char name1[100]="";
            char *lastname;
            char lastname1[100]="";
            char *email;
            char email1[100];
            parts=strtok(line_repeal,":");
            strcpy(parts1,parts);
            if(strcmp(parts1,"Korisnik")==0)
            {
                user=strtok(NULL," ");
                strcpy(user1,user);
                pw=strtok(NULL,",");
                //strcpy(pw1,pw);
                name=strtok(NULL," ");
                strcpy(name1,name);
                lastname=strtok(NULL," ");
                strcpy(lastname1,lastname);
                email=strtok(NULL," ");
                strcpy(email1,email);
                strcpy(pw1,new_pw);
                fprintf(fp4,"%s:%s %s,%s %s %s",parts1,user1,pw1,name1,lastname1,email1);
            }
            else if(strcmp(parts1,"Klijent")==0)
            {
                user=strtok(NULL," ");
                strcpy(user1,user);
                pw=strtok(NULL,",");
                name=strtok(NULL," ");
                strcpy(name1,name);
                lastname=strtok(NULL," ");
                strcpy(lastname1,lastname);
                strcpy(pw1,new_pw);
                fprintf(fp4,"%s:%s %s,%s %s",parts1,user1,pw1,name1,lastname1);
            }
            else if(strcmp(parts1,"Admin")==0)
            {

                user=strtok(NULL," ");
                strcpy(user1,user);
                pw=strtok(NULL,",");

                name=strtok(NULL," ");
                if(name != NULL)
                    strcpy(name1,name);

                lastname=strtok(NULL," ");
                if(lastname!=NULL)
                    strcpy(lastname1,lastname);

                strcpy(pw1,new_pw);
                if(name!=NULL && lastname1!=NULL)
                {
                    fprintf(fp4,"%s:%s %s,%s %s\n",parts1,user1,pw1,name1,lastname1);
                }
                else
                {
                    fprintf(fp4,"%s:%s %s\n",parts1,user1,pw1);
                }

            }

        }
        else
        {
            fputs(line_repeal,fp4);
        }

    }
    fclose(fp5);
    fclose(fp4);
    remove("users.txt");
    rename("privremeni.txt","users.txt");
    FILE *fp=fopen("users_repeal_pw.txt","r");
    FILE *fp1=fopen("nova.txt","a+");
    char line[100];
    while(fgets(line,sizeof(line),fp)!=NULL)
    {
        if(strstr(line,username)!=NULL)
        {}
        else
        {
            fputs(line,fp1);
        }

    }
    fclose(fp);
    fclose(fp1);
    remove("users_repeal_pw.txt");
    rename("nova.txt","users_repeal_pw.txt");

}

int check_if_username_exist(char *username)
{
    char line[100];
    int counter=0;
    FILE *fp=fopen("users.txt","r");
    while(fgets(line,sizeof(line),fp)!=NULL)
    {
        if(strstr(line,username)!=NULL)
        {
            counter++;
        }
    }
    fclose(fp);
    if(counter!=0)
    {
        printf("Korisnicko ime vec postoji!\n");
        return 1;
    }
}

void adminOptions()
{
    char str[10];
    do{
        printf("Type  'Options'  to show available options, or  'Exit'  to log out!  ");
        scanf("%s", str);
    } while (strcmp(str, "Options") != 0 && strcmp(str, "Exit") != 0);

    if(strcmp(str, "Options") == 0)
    {
        clrscr();
        printf("Please, choose an option below:\n\n");
        admin_interface();
    }

    else if(strcmp(str, "Exit") == 0)
    {
        clrscr();
        main();
    }

}

void userOption(char *name, char* username)
{
    char choice[10];
    printf("Type  'Back'  to go back, or  'Exit'  to log out!\n\n");
    do{
        printf("Your choice: ");
        scanf("%s", choice);
    } while (strcmp(choice, "Back") != 0 && strcmp(choice, "Exit") != 0);

    if(strcmp(choice, "Back")==0)
    {
        clrscr();
        printf("Please, choose an option below:\n");
        user_interface(name, username);
    }

    else if(strcmp(choice, "Exit")==0)
    {
        clrscr();
        main();
    }
}

void clientOption(char* name, char* username)
{
    char str[10];
    printf("Type  'Back'  to go back, or  'Exit'  to log out!\n\n");
    do{
        printf("Your choice: ");
        scanf("%s", str);
    } while (strcmp(str, "Back") != 0 && strcmp(str, "Exit") != 0);

    if(strcmp(str, "Back") == 0)
    {
        clrscr();
        printf("Please, choose an option below:\n");
        client_interface(name, username);
    }

    else if(strcmp(str, "Exit") == 0)
    {
        clrscr();
        main();
    }

}

void manageRequests()
{
    clrscr();
    FILE* fr=fopen("requests.txt", "r");
    char line[300];
    char izbor[20];
    printf("\nYou have following requests:\n\n");
    while (fgets(line, sizeof(line), fr) != NULL)
            printf("%s", line);

    fclose(fr);

    do{
        printf("\nType  'Delete'  to delete request or  'Back'  to go back: ");
        scanf("%s", izbor);
    } while(strcmp(izbor, "Delete")!=0 && strcmp(izbor, "Back")!=0);
    if(strcmp(izbor, "Delete")==0)
    {
        char iD[100]="RequestNum ";
        char idd[10]="";
        char line1[300];
        printf("Type the ID of request you want to delete: ");
        scanf("%s", &idd);
        strcat(iD, idd);

        FILE* f11=fopen("requests.txt", "r");
        FILE* f12=fopen("temp.txt", "a");
        while (fgets(line1, sizeof(line1), f11) != NULL) {
            line[strcspn(line1, "\n")]=0;
            if (strstr(line1, iD) == NULL)
                fprintf(f12, "%s", line1);

        }

        fclose(f11);
        fclose(f12);
        remove("requests.txt");
        rename("temp.txt", "requests.txt");
        printf("\nUspjesno ste obrisali zahtjev.\n\n");
        adminOptions();

    }
    if(strcmp(izbor, "Back")==0)
    {
        clrscr();
        printf("Please, choose an option below:\n\n");
        admin_interface();
    }


}

void printEvents()
{
    clrscr();
    FILE* file;
    char s[200];
    if ((file = fopen("events.txt", "r")) != NULL)
    {
        printf("Available events:\n\n");
        while (fgets(s, 200, file) != NULL)
            printf("%s", s);
            printf("\n");
        fclose(file);
    }
    else printf("Error opening file\n");

}

void printTicket(int id, char eventName[], char date[], char vrijeme[], char price[], char *name)
{
    char str[25]="";
    strcat(str, "ulaznica");
    char c[10];
    itoa(id, c, 10);
    strcat(str, c);
    strcat(str, ".txt");
    FILE* fout= fopen(str, "w");
    fprintf(fout, "=======================================\n");
    fprintf(fout, "Ticket ID: %d \n%s%s%s%s\nKupac: %s", id, eventName, date, vrijeme, price, name);
    fprintf(fout, "\n=======================================\n");
    fclose(fout);

}

void printSoldTickets(char* name, char* username)
{
    clrscr();
    printf("Prodate ulaznice:\n\n");
    char d1[5];
    char line[500];
    FILE* file11 = fopen("num.txt", "r");
        fgets(d1, sizeof(d1), file11);
    fclose(file11);
    int num = atoi(d1);
    int count=0;
    FILE *files[num];
    for (int i = 1; i <= num; i++)
    {
        char d2[5];
        sprintf(d2, "%d", i);
        char filename[20];
        sprintf(filename, "%s%s.txt", "ulaznica", d2);
        if((files[i] = fopen(filename, "r")) != NULL){
            printf("%s\n", filename);
            while (fgets(line, sizeof(line), files[i]) != NULL)
                printf("%s", line);
        }
        else count++;
        fclose(files[i]);
    }

    if(count!=num){
    char strC[20];
    printf("Type  'Delete'  to delete a ticket,  'Back'  to go back!\n\n");
    do{
        printf("Your choice:  ");
        scanf("%s", &strC);
    } while(strcmp(strC, "Delete")!=0 && strcmp(strC, "Back")!=0);

    if(strcmp(strC, "Delete") == 0){
        char name[20];
        int k;
        do{
            printf("Type the name of ticket you wish to delete: ");
            scanf("%s", name);
            k=remove(name);
        } while(k==-1);

        printf("\nYou have successfully deleted ticket: %s\n\n", name);
        clientOption(name, username);

    }

    else if(strcmp(strC, "Back") == 0){
        clrscr();
        printf("Please choose an option below:\n");
        client_interface(name, username);
    }
    }
    else{
        printf("\nNo sold tickets.\n\n\n");
        clientOption(name, username);
    }

}

void myTickets(char* name, char* username)
{
    clrscr();
    char name1[100];
    strcpy(name1, name);
    clrscr();
    char d1[5];
    FILE* file11 = fopen("num.txt", "r");
    fgets(d1, sizeof(d1), file11);
    fclose(file11);
    int num = atoi(d1);
    int count=0;
    FILE *files1[num];
    printf("Vase ulaznice:\n\n");
    char line1[300];
    for (int i = 1; i <= num; i++)
    {
        char d22[5];
        sprintf(d22, "%d", i);
        char filename1[20];
        sprintf(filename1, "%s%s.txt", "ulaznica", d22);
        if((files1[i] = fopen(filename1, "r")) != NULL){
        while (fgets(line1, sizeof(line1), files1[i]) != NULL){
            if(strstr(line1, name1) != NULL){
                for(int k=0; k<5; k++){
                    if(fgets(line1, sizeof(line1), files1[i]))
                        printf("%s", line1);
                    }
                    printf("\n");
                }

            }
        rewind(files1[i]);
        fclose(files1[i]);
        }
    }

    printf("Type  'Delete'  to delete your ticket, or  'Back' to go back!\n");
    char choice[10];
    do{
        printf("\nYour choice: ");
        scanf("%s", choice);
    } while(strcmp(choice, "Delete")!=0 &&strcmp(choice, "Back")!=0);

    if(strcmp(choice, "Delete")==0)
    {
        char name1[100];
        strcpy(name1, name);
        clrscr();
        char d1[5];
        FILE* file11 = fopen("num.txt", "r");
        fgets(d1, sizeof(d1), file11);
        fclose(file11);
        int num = atoi(d1);
        int count1=0;
        FILE *files1[num];
        printf("Vase ulaznice:\n\n");
        char line1[300];
        for (int i = 1; i <= num; i++)
        {
            char d22[5];
            sprintf(d22, "%d", i);
            char filename1[20];
            sprintf(filename1, "%s%s.txt", "ulaznica", d22);
            if((files1[i] = fopen(filename1, "r")) != NULL){
            while (fgets(line1, sizeof(line1), files1[i]) != NULL){
                if(strstr(line1, name1) != NULL){
                    for(int k=0; k<5; k++){
                       if(fgets(line1, sizeof(line1), files1[i]))
                            printf("%s", line1);
                    }
                            printf("\n");
                }

            }
                rewind(files1[i]);
                fclose(files1[i]);
            }
        }

    char id8[10];
    printf("Type ID of the ticket you wish to delete: "); scanf("%s", id8);
    for (int i = 1; i <= num; i++)
    {

        char d22[5];
        sprintf(d22, "%d", i);
        char filename1[20];
        sprintf(filename1, "%s%s.txt", "ulaznica", d22);
        if((files1[i] = fopen(filename1, "r")) != NULL){
        while (fgets(line1, sizeof(line1), files1[i]) != NULL){
            if(strstr(line1, id8) != NULL){
                count1=i;
                }
            }

        }
        fclose(files1[i]);
    }
    if(count1 != 0){
        char d23[5];
        sprintf(d23, "%d", count1);
        char filename2[20];
        sprintf(filename2, "%s%s.txt", "ulaznica", d23);
        remove(filename2);
        printf("\nYou deleted your ticket!\n\n");
        userOption(name, username);
        }
    else if(count1==0)
    {
        printf("\nError!\n\n");
        userOption(name, username);
    }
    }
    else if(strcmp(choice, "Back")==0)
    {
        clrscr();
        printf("Please, choose an option below:\n");
        user_interface(name, username);
    }

}

void myCredits(char* name, char* username)
{
    clrscr();
    FILE* fcredits = fopen("credits.txt", "r");
    rewind(fcredits);
    char line[100];
    char cred[100]={'\0'};
    while (fgets(line, sizeof(line), fcredits) != NULL) {
        line[strcspn(line, "\n")]=0;
        if (strstr(line, username) != NULL)
            {
                for(int i=0; i<1; i++)
                {
                    if(fgets(line, sizeof(line), fcredits))
                        strcpy(cred, line);
                }
                break;
            }
    }
    fclose(fcredits);
    printf("\nAvailable credits: %s\n\n", cred);
    userOption(name, username);

}

int searchEvent(char strr[], char* name)
{
    FILE* fp=fopen("events.txt", "r");
    char line[1024];
    int count=0;
    while (fgets(line, sizeof(line), fp) != NULL) {
        line[strcspn(line, "\n")]=0;
        if (strcmp(line, strr) == 0) {
            count++;
            return 1;
        }
    }
    fclose(fp);

    if(count == 0)
    {
        return -1;
    }

}

int enoughCredits(char strr[], char* username)
{
    FILE* fp=fopen("credits.txt", "r");
    char line[100];
    char cred[100];
    while (fgets(line, sizeof(line), fp) != NULL) {
        line[strcspn(line, "\n")]=0;
        if (strstr(line, username) != NULL)
            {
                for(int i=0; i<1; i++)
                {
                    if(fgets(line, sizeof(line), fp))
                        strcpy(cred, line);
                }
            }
    }
    fclose(fp);

    sscanf(cred, "%d", &credits);

    FILE* fp1=fopen("events.txt", "r");
    char line1[1024];
    char str1[100]="";
    while (fgets(line, sizeof(line), fp1) != NULL) {
        line[strcspn(line, "\n")]=0;
         if (strcmp(line, strr) == NULL)
            {
                for(int i=0; i<6; i++)
                {
                    if(fgets(line, sizeof(line), fp))
                        strcpy(str1, line);
                }

            }
    }
    char str2[50];
    char *token = strtok(str1, " ");
    while(token != NULL ) {
      strcpy(str2, token);
      token = strtok(NULL, " ");
    }

    int cena;
    sscanf(str2, "%d", &cena);
    fclose(fp1);

    if(credits < cena) return -1;
    else
    {
        int fin=credits-cena;
        char c1[10];
        itoa(fin, c1, 10);

        char *fname = "credits.txt";
        char *temp = "temp.txt";
        FILE* fp3=fopen(fname, "r");
        FILE* fp4=fopen(temp, "w");
        char line[500];
        while (fgets(line, sizeof(line), fp3) != NULL) {
            line[strcspn(line, "\n")]=0;
            if (strstr(line, username) != NULL)
            {
                fprintf(fp4, "%s\n", line);
                fgets(line, sizeof(line), fp3);
                fprintf(fp4, "%s\n", c1);
            }
            else
                fprintf(fp4, "%s\n", line);
    }
    fclose(fp3);
    fclose(fp4);
    remove(fname);
    rename(temp, fname);
    }

}

void buyTicket(char* name, char* username)
{
    clrscr();
    printEvents();
    char event_id[20];
    printf("Type  'Back'  to go back, or enter the ID of the event you want to attend.\n");
    printf("\nYour choice: ");
    scanf("%s", &event_id);
    if(strcmp(event_id, "Back")==0)
    {
        clrscr();
        printf("Please, choose an option below:\n");
        user_interface(name, username);
    }
    else{
    char str3[20]="ID: ";
    strcat(str3, event_id);
    int k = searchEvent(str3, name);

    int j = enoughCredits(str3, username);
    if(j == -1){
        printf("Nedovoljno kredita.\n\n");
        k=2;
        userOption(name, username);
    }

    if(k == 1) {
    FILE* fp=fopen("events.txt", "r");
    char line[2048], eventName[30], opis[100], date[30], vr[20], info[30], price[30];
    srand(time(0));
    int id = rand();
    char c[10];
    itoa(id, c, 10);
    // Read the file line by line until the end of the file
    while (fgets(line, sizeof(line), fp) != NULL) {
        // Check if the current line is the one we are looking for
        if (strstr(line, str3) != NULL) {

            fgets(line, sizeof(line), fp);
                 strcpy(eventName, line);
            fgets(line, sizeof(line), fp);
                 strcpy(opis, line);
            fgets(line, sizeof(line)-2, fp);
                 strcpy(date, line);
            fgets(line, sizeof(line), fp);
                 strcpy(vr, line);
            fgets(line, sizeof(line), fp);
                 strcpy(info, line);
            fgets(line, sizeof(line), fp);
                 strcpy(price, line);

            char d[5];
            FILE* file1 = fopen("num.txt", "r");
                fgets(d, sizeof(d), file1);
                fclose(file1);
            FILE* file4 = fopen("num.txt", "w");
                int num = atoi(d);
                num++;
                sprintf(d, "%d", num);
                fputs(d, file1);
            fclose(file4);

            char strs[]="ulaznica";
            strcat(strs, d);
            strcat(strs, ".txt");
            FILE* file3=fopen(strs, "w");
                struct tm today = get_today();
                fprintf(file3, "Datum kupovine: %d.%d.%d\n", today.tm_mday, today.tm_mon + 1, today.tm_year + 1900);
                fprintf(file3, "Kupac: %s\nTicket ID: %d \n%s%s%s%s\n\n",name, id, eventName, date, vr, price);
            fclose(file3);

            break;
        }
    }

    int claiming;
    do{
        printf("Claim ticket in person [1], or online [2]: ");
        scanf("%d", &claiming);
    } while(claiming < 1 || claiming > 2);
    if(claiming==1)
    {
        printf("Vasu ulaznicu mozete preuzeti na mjestu odrzavanja dogadjaja.\n\n");
        userOption(name, username);
    }
    else if(claiming == 2)
    {
        printf("Vasa ulaznica je spremna za printanje. Fajl: ulaznica%s\n\n", c);
        printTicket(id, eventName, date, vr, price, name);
        userOption(name, username);
    }
    fclose(fp);
    }

    else if(k == -1)
    {
        buyTicket(name, username);
    }
    }
}

void createEvent(char* name, char* username)
{
    FILE* fp = fopen("events.txt", "a");
    Event Event;

    srand(time(0));
    int id = rand();

    printf("\nNaziv dogadjaja: ");
        getchar(); scanf("%[^\n]", Event.name);
    printf("Opis dogadjaja: ");
        getchar(); scanf("%[^\n]", Event.descr);
    printf("Lokacija odrzavanja: ");
        getchar(); scanf("%[^\n]", Event.place);
    printf("Datum [DD.MM.YYYY]: ");
        scanf("%s", Event.date);
    printf("Vrijeme [H:M]: ");
        scanf("%s", Event.time);
    printf("Kupuje se na ime [Da/Ne]: ");
        scanf("%s", Event.buyOnName);
    printf("Cijena [BAM]: ");
        scanf("%f", &Event.price);

    fprintf(fp, "ID: %d\nNaziv: %s\nOpis: %s\nDatum: %s\nVrijeme: %s\nKupuje se na ime: %s\nCijena: %.2f\nKreirao: %s\n\n", id, Event.name, Event.descr, Event.date, Event.time, Event.buyOnName, Event.price, name);    fclose(fp);
    printf("\nSuccessfully created an event.\n\n");
    clientOption(name, username);
}

void sendRequest(char* name, char* username)
{
    clrscr();
    char strID[10]="ID: ";
    char strNum[10]="";
    printEvents();

    srand(time(0));
    int req_id = rand();

    int k;
    printf("Type  'Back'  to go back, or ID of the event you want to delete!\n\n");
    printf("Your choice: ");
    scanf("%s", &strNum);
    if(strcmp(strNum, "Back")==0)
    {
        clrscr();
        printf("Please, choose an option below:\n");
        client_interface(name, username);
    }
    else{
    strcat(strID, strNum);
    k = searchEvent(strID, name);
    if(k==-1)
    {
        clrscr();
        sendRequest(name, username);
    }
    else if (k!=-1)
    {
        FILE *fprequest=fopen("requests.txt", "a");
        fprintf(fprequest,"RequestNum %d: %s je poslao zahtjev za uklanjanje dogadjaja sa %s\n", req_id, name, strID);
        fclose(fprequest);
        printf("\nUspjesno ste poslali zahtjev za brisanje dogadjaja.\n\n");
        clientOption(name, username);
    }
    }
}

void extract_date(char str[], char date[]) {
    sscanf(str, "Datum kupovine: %s", date);
}

Date string_to_date(const char *str) {
    Date date;
    sscanf(str, "%d.%d.%d", &date.day, &date.month, &date.year);
    return date;
}

int compare_dates(Date date1, Date date2) {
    if (date1.year < date2.year) {
        return -1;
    } else if (date1.year > date2.year) {
        return 1;
    } else if (date1.month < date2.month) {
        return -1;
    } else if (date1.month > date2.month) {
        return 1;
    } else if (date1.day < date2.day) {
        return -1;
    } else if (date1.day > date2.day) {
        return 1;
    } else {
        return 0;
    }
}

int is_date_between(Date date, Date start_date, Date end_date) {
    int start_compare = compare_dates(date, start_date);
    int end_compare = compare_dates(date, end_date);
    if (start_compare >= 0 && end_compare <= 0) {
        return 1;
    }
    else {
        return 0;
    }
}

void createReport(char* name, char* username)
{
    clrscr();
    printEvents();
    char naz[50];
    printf("Type  'Back'  to go back, or the name of the event you want to get report about: "); scanf("%s", naz);
    if(strcmp(naz, "Back")==0)
    {
        clrscr();
        printf("Please, choose an option below:\n");
        client_interface(name, username);
    }
    else{
    clrscr();
    FILE *report;
    report = fopen("report.txt", "w");
    fprintf(report, "    Report for the event named: %s\n\n", naz);

    Date start_date, end_date;
    char input[11];
    printf("Enter start date[dd.mm.yyyy]: ");
    scanf("%s", input);
    sscanf(input, "%d.%d.%d", &start_date.day, &start_date.month, &start_date.year);

    printf("Enter end date[dd.mm.yyyy]: ");
    scanf("%s", input);
    sscanf(input, "%d.%d.%d", &end_date.day, &end_date.month, &end_date.year);


    char str[]="Datum kupovine: ";
    char str1[]="Cijena: ";
    int totalSales=0;
    int totalProfit=0;

    char d1[5];
    FILE* file11 = fopen("num.txt", "r");
    fgets(d1, sizeof(d1), file11);
    fclose(file11);
    int num = atoi(d1);
    int count=0;

    FILE *files1[num];
    char line[300];
    for (int i = 1; i <= num; i++)
    {
        char d22[5];
        sprintf(d22, "%d", i);
        char filename1[20];
        sprintf(filename1, "%s%s.txt", "ulaznica", d22);
        if((files1[i] = fopen(filename1, "r")) != NULL){
        while (fgets(line, sizeof(line), files1[i]) != NULL) {
        if (strstr(line, str) != NULL)
            {
                char date[11];
                extract_date(line, date);
                Date date1 = string_to_date(date);
                int result = is_date_between(date1, start_date, end_date);
                if (result) {
                for(int k=0; k<6; k++)
                {
                    if(fgets(line, sizeof(line), files1[i]))
                    {
                        if (strstr(line, naz) != NULL)
                        {
                            totalSales++;
                            for(int j=0; j < 3; j++)
                            {
                                if(fgets(line, sizeof(line), files1[i])){
                                if(strstr(line, str1) != NULL)
                                {
                                    char *ptr;
                                    int value = strtol(line + 8, &ptr, 10);
                                    totalProfit+=value;
                                }
                                }
                            }
                        }

                    }

                }
                }

            }

        }
    fclose(files1[i]);
    }

    }


    fprintf(report, "Total sales: %d\nTotal profit: %d BAM", totalSales, totalProfit);
    fprintf(report, "\n");
    printf("\nCreated a report.txt.\n\n");
    fclose(report);
    clientOption(name, username);
    }
}



void admin_interface()
{
    printf("1. Kreiraj klijentski nalog\n2. Kreirajte administratorski nalog\n3. Aktivacija naloga\n4. Suspendovanje naloga\n5. Brisanje naloga\n6. Ponistavanje sifre\n7. Brisanje klijentskih dogadjaja\n8. Blokiranje klijentskog dogadjaja\n9. Zahtjevi\n10. Odjava sa naloga\n\n");
    int choice_admin;
    do{
        printf("Enter your choice: ");
        scanf("%d", &choice_admin);
    } while(choice_admin < 1 || choice_admin > 10);

    if(choice_admin==1)
    {
        char client_name[50];
        char client_pw[50];
        char client_real_name[50];
        char client_real_surname[50];
        clrscr();
        printf("Dobro dosli u kreiranje klijentskog naloga!\n\n");
        do
        {
            printf("Unesite korisnicko ime klijenta: ");
            scanf("%s", client_name);

        }while(check_if_username_exist(client_name) == 1);

        printf("Unesite lozinku klijenta: ");
        scanf("%s",client_pw);
        printf("Unesite ime klijenta: ");
        scanf("%s",client_real_name);
        printf("Unesite prezime klijenta: ");
        scanf("%s",client_real_surname);
        //Potrebno je vjerovatno izvrsiti provjeru da li taj klijent vec postoji.

        FILE *fp = fopen("users.txt", "a");
        char exit_admin[50];

        fseek(fp,0,SEEK_END);
        fprintf(fp,"Klijent:%s %s,%s %s\n",client_name,client_pw,client_real_name,client_real_surname);
        fflush(fp);
        fclose(fp);
        printf("\nUspjesno ste se kreirali klijentski nalog!\n\n");
        FILE *fp_for_n_login=fopen("n_login.txt","a+");
        fprintf(fp_for_n_login,"%s %d\n",client_name,0);
        fclose(fp_for_n_login);
        adminOptions();

    }
    else if(choice_admin==2)
    {
        char admin_name[50];
        char admin_pw[50];
        char admin_real_name[50];
        char admin_real_surname[50];
        clrscr();
        printf("Dobro dosli u kreiranje administratorskog naloga!\n\n");

        do
        {
            printf("Unesite korisnicko ime admina: ");
            scanf("%s",admin_name);

        }while(check_if_username_exist(admin_name) == 1);

        printf("Unesite lozinku admina: ");
        scanf("%s",admin_pw);
        printf("Unesite ime admina: ");
        scanf("%s",admin_real_name);
        printf("Unesite prezime admina: ");
        scanf("%s",admin_real_surname);
        //Potrebno je vjerovatno izvrsiti provjeru da li taj admin vec postoji.

        FILE *fp = fopen("users.txt", "a+");
        char exit_admin[50];

        fseek(fp,0,SEEK_END);
        fprintf(fp,"Admin:%s %s,%s %s\n",admin_name,admin_pw,admin_real_name,admin_real_surname);
        fflush(fp);
        fclose(fp);
        printf("\nUspjesno ste se kreirali administratorski nalog!\n\n");
        FILE *fp_for_n_login=fopen("n_login.txt","a+");
        fprintf(fp_for_n_login,"%s %d\n",admin_name,0);
        fclose(fp_for_n_login);
        adminOptions();
    }
    else if(choice_admin==3)
    {
        clrscr();
        //Aktiviranje suspendovanog korisničkog naloga
        char activate_name[50];
        char str[50];
        char line[100];
        int counter=0;
        printf("\nUnesite korisnicko ime suspendovanog naloga kojeg zelite da aktivirate: ");
        scanf("%s",activate_name);
        FILE *fp=fopen("suspended_users.txt","r");
        FILE *fp2=fopen("suspended_users2.txt","a+");
        while(fgets(line,sizeof(line),fp)!=NULL)
        {
            if(strstr(line,activate_name)!=NULL)
            {
                counter++;
            }
            else
            {
                fputs(line,fp2);
            }

        }
        fclose(fp);
        fclose(fp2);
        remove("suspended_users.txt");
        rename("suspended_users2.txt","suspended_users.txt");
        if(counter==0)
        {
            printf("\nNe postoji suspendovani korisnicki nalog %s !\n\n",activate_name);
        }
        else
        {
            printf("\nUspjesno ste aktivirali korisnicki nalog %s !\n\n",activate_name);
        }

        adminOptions();

    }
    else if(choice_admin==4)
    {
        clrscr();
        printf("Suspendovanje korisnickog naloga.\n\n");
        char username_to_suspend[50];
        printf("Unesite username naloga kojeg zelite da suspendujete: ");
        scanf("%s",username_to_suspend);
        char line[100];
        char new_line[sizeof(line)];
        FILE *fp = fopen("users.txt", "r+");
        FILE *fp2=fopen("suspended_users.txt","a+");
        while(fgets(line,sizeof(line),fp)!=NULL)
        {

            if(strstr(line,username_to_suspend)!=NULL)
            {

                fputs(line,fp2);
            }
            else
            {
                continue;
            }
            break;

        }

        fclose(fp2);

        fclose(fp);
        printf("\nNalog je suspendovan!\n\n");
        adminOptions();


    }
    else if(choice_admin==5)
    {
        //TODO brisanje naloga!
        char str[50];
        clrscr();
        char delete_acc[50];
        printf("Unesite korisnicko ime naloga kojeg zelite da obrisete: ");
        scanf("%s",delete_acc);
        //Potrebno je izvrsiti jos dodatnu provjeru neku
        FILE *fp=fopen("users.txt","r");
        FILE *fp2=fopen("users2.txt","a+");
        char line[100];
        while(fgets(line,sizeof(line),fp)!=NULL)
        {

            if(strstr(line,delete_acc)!=NULL)
            {}
            else
            {
                fputs(line,fp2);
            }
        }
        fclose(fp);
        fclose(fp2);
        remove("users.txt");
        rename("users2.txt","users.txt");

        FILE *fp3=fopen("n_login.txt", "r");
        FILE *fp4=fopen("n_login2.txt", "a+");
        while(fgets(line,sizeof(line),fp3)!=NULL)
        {
            if(strstr(line,delete_acc)!=NULL)
            {}
            else
            {
                fputs(line,fp4);
            }
        }

        fclose(fp3);
        fclose(fp4);
        remove("n_login.txt");
        rename("n_login2.txt","n_login.txt");

        FILE *fp5=fopen("credits.txt", "r");
        FILE *fp6=fopen("credits2.txt", "a+");
        while(fgets(line,sizeof(line),fp5)!=NULL)
        {
            if(strstr(line,delete_acc)!=NULL)
            {
                fgets(line,sizeof(line),fp5);
            }
            else
            {
                fputs(line,fp6);
            }
        }

        fclose(fp5);
        fclose(fp6);
        remove("credits.txt");
        rename("credits2.txt","credits.txt");

        printf("\nUspjesno ste izvrsili brisanje korisnickog naloga!\n\n");
        adminOptions();
    }
    else if(choice_admin==6)
    {
        int counter=0;
        char username_pw_repeal[50];
        char line[100];
        char str[50];
        printf("Unesite korisnicko ime naloga kojem zelite da ponistite sifru: ");
        scanf("%s",username_pw_repeal);
        FILE *fp=fopen("users.txt","r");
        FILE *fp2=fopen("users_repeal_pw.txt","a+");
        while(fgets(line,sizeof(line),fp)!=NULL)
        {
            if(strstr(line,username_pw_repeal)!=NULL)
            {
                fputs(line,fp2);
                counter++;
            }
        }
        fclose(fp);
        fclose(fp2);
        if(counter!=0)
        {
            printf("Uspjesno ste izvrsili ponistavanje sifre korisniku cije je korisnicko ime: %s\n\n",username_pw_repeal);
            adminOptions();
        }
        else
        {
            printf("Korisnicko ime koje ste unijeli ne postoji!\n\n");
            adminOptions();
        }

    }
    else if(choice_admin==7)
    {
        //TODO brisanje klijentskog doagađaja.
        int line_counter=0;

        clrscr();
        char ID_of_Event[50];
        char line[100];
        printEvents();
        printf("\nUnesite ID dogadjaja koji zelite da obrisete: ");
        scanf("%s",ID_of_Event);
        FILE *fp=fopen("events.txt","r");
        FILE *fp2=fopen("events2.txt","a+");
        while(fgets(line,sizeof(line),fp)!=NULL)
        {
            if(strstr(line,ID_of_Event)!=NULL)
            {
                const int num_lines_to_skip = 7;
                for (int i = 0; i < num_lines_to_skip; i++)
                {
                    if (fgets(line, sizeof(line), fp) == NULL)
                    {
                    // Ako smo došli do kraja datoteke, prekidamo petlju
                        break;
                    }
                    line_counter++;
                }

            }
            else
            {
                fputs(line,fp2);
                line_counter++;

            }
        }
        fclose(fp);
        fclose(fp2);
        remove("events.txt");
        rename("events2.txt","events.txt");
        printf("\nUspjesno ste izvrsili brisanje klijentskog dogadjaja ciji je ID: %s !\n",ID_of_Event);
        char str[50];
        char str2[50];
        do
        {
            printf("\nZa prikaz klijentskih dogadjaja ukucajte 'Prikazi'.\nZa povratak na pocetnu stranicu ukucajte 'Back' !\n");
            scanf("%s", str);

        } while(strcmp(str,"Back")!= 0 && strcmp(str,"Prikazi")!=0);

        if(strcmp(str,"Back")== 0)
        {
            clrscr();
            admin_interface();
        }
        if(strcmp(str,"Prikazi")==0)
        {
            clrscr();
            printEvents();
            adminOptions();
        }

    }
    else if(choice_admin==8)
    {
        //Blokiranje klijentskog dogadjaja
        clrscr();
        int line_counter=0;
        char line[100];
        char ID_event_to_block[50];
        printEvents();
        printf("\nUnesite ID dogadjaja kojeg zelite da blokirate: ");
        scanf("%s",ID_event_to_block);
        FILE *fp=fopen("events.txt","r");
        FILE *fp2=fopen("suspended_events.txt","a+");
        FILE *fp3=fopen("events2.txt","a+");
        while(fgets(line,sizeof(line),fp)!=NULL)
        {
            if(strstr(line,ID_event_to_block)!=NULL)
            {
                const int num_lines = 7;
                for (int i = 0; i < num_lines; i++)
                {
                    fputs(line,fp2);
                    if (fgets(line, sizeof(line), fp) == NULL)
                    {
                    // Ako smo došli do kraja datoteke, prekidamo petlju
                        break;
                    }
                    line_counter++;
                }
                    fputs("\n",fp2);

            }
            else
            {
                fputs(line,fp3);
            }
        }
        fclose(fp);
        fclose(fp2);
        fclose(fp3);
        remove("events.txt");
        rename("events2.txt","events.txt");
        printf("\nUspjesno ste izvrsili blokiranje klijentskog dogadjaja ciji je ID: %s !\n",ID_event_to_block);
        char str[50];
        char str2[50];
        do
        {
            printf("\nZa prikaz klijentskih dogadjaja ukucajte 'Prikazi'.\nZa povratak na pocetnu stranicu ukucajte 'Back' !\n");
            scanf("%s", str);

        } while(strcmp(str,"Back")!= 0 && strcmp(str,"Prikazi")!=0);

        if(strcmp(str,"Back")== 0)
        {
            clrscr();
            admin_interface();
        }
        if(strcmp(str,"Prikazi")==0)
        {
            clrscr();
            printEvents();
            adminOptions();
        }

    }
    else if(choice_admin==9)
    {
        manageRequests();
    }
    else if(choice_admin==10)
    {
        clrscr();
        main();
    }


}

void client_interface(char* name, char* username)
{
    printf("\n 1. Kreiraj dogadjaj\n 2. Pregled prodatih ulaznica\n 3. Brisanje dogadjaja\n 4. Izvjestaj\n 5. Odjava sa naloga\n");

    int choice_client;
    do
    {
        printf("\nEnter your choice: ");
        scanf("%d", &choice_client);

    } while(choice_client < 1 || choice_client > 5);

    if(choice_client == 1)
    {
        createEvent(name, username);
    }

    else if(choice_client == 2)
    {
        printSoldTickets(name, username);
    }
    else if(choice_client == 3)
    {
        sendRequest(name, username);
        // send request to admin to delete an event
    }
    else if(choice_client == 4)
    {
        createReport(name, username);
    }
    else if(choice_client == 5)
    {
        clrscr();
        main();
    }

}

void user_interface(char *name, char* username)
{
    printf("\n 1. Pregledaj dogadjaje\n 2. Kupi ulaznicu\n 3. Moje ulaznice\n 4. Moji krediti\n 5. Odjava sa naloga\n");
    int choice_user;
    do
    {
        printf("\nEnter your choice: ");
        scanf("%d", &choice_user);

    } while(choice_user < 1 || choice_user > 5);

    if(choice_user == 1)
    {
        printEvents();
        char choice[10];
        printf("Type  'Buy'  to buy ticket, 'Back'  to go back.\n");
        do{
            printf("\nYour choice: ");
            scanf("%s", choice);
        } while(strcmp(choice, "Buy") != 0 && strcmp(choice, "Back") != 0);

        if(strcmp(choice, "Buy")==0)
            buyTicket(name, username);

        else if(strcmp(choice, "Back")==0)
        {
            clrscr();
            printf("Please, choose an option below:\n");
            user_interface(name, username);
        }

    }
    else if(choice_user == 2)
    {
        buyTicket(name, username);
    }
    else if(choice_user == 3)
    {
        myTickets(name, username);
    }
    else if(choice_user == 4)
    {
        myCredits(name, username);
    }
    else if(choice_user == 5)
    {
        clrscr();
        main();
    }
}



int main()
{
    int choice;
    printf("**************************************************************\n\n");
    printf("        Welcome to the EventTickets application!\n\n");
    printf("**************************************************************\n");
    printf(" 1. Login\n 2. Register\n 3. Exit\n");

    do{
        printf("\nEnter your choice: ");
        scanf("%d", &choice);
    } while(choice < 1 || choice > 3);

    if (choice == 1)
    {
        static int var=0;
        clrscr();
        printf("**************************************************************\n\n");
        printf("    Login to EventTickets application!\n\n");
        printf("**************************************************************\n");
        char username[50];
        printf("Enter your username: ");
        scanf("%s", username);
        char line[100];
        printf("Enter your password: ");
        char password[50];
        scanf("%s", password);
        int cou=0;
        int cou_for_repeal=0;
        char logout[50];
        FILE *fp2=fopen("suspended_users.txt","r");
        while(fgets(line,sizeof(line),fp2)!=NULL)
        {

            if(strstr(line,username)!=NULL)
            {
                cou++;

            }
        }
        fclose(fp2);
        FILE *fp3=fopen("users_repeal_pw.txt","r+");
        while(fgets(line,sizeof(line),fp3)!=NULL)
        {
            if(strstr(line,username)!=NULL)
            {
                cou_for_repeal++;

            }

        }
        fclose(fp3);
        if(cou!=0)
        {
            printf("Vas nalog je suspendovan!\n\n");
            Exit();
        }
        else if(cou_for_repeal!=0)
        {
            //Promjena sifre.
            function_repeal(username);
            clrscr();
            main();
        }
        else
        {
            FILE *fp = fopen("users.txt", "r");
            char type[50] = "";
            char username_from_file[50] = "";
            char password_from_file[50] = "";
            char real_name[50]="";
            int read_type=1;
            int read_real_name=0;
            int read_name=0;
            int read_pw=0;

        while(fgets(line,sizeof(line),fp)!=NULL)
        {

               if(strstr(line,username)!=NULL && strstr(line,password)!=NULL)
               {
                    line[strcspn(line,"\n")]=0;
                    //printf("%s",line);
                    int counter=0;
                    int ch;
                    int i = 0;
                    for(i=0;line[i]!='\0';i++)
                    {
                        if(counter<=2)
                        {
                            if (line[i] == ':')
                            {
                                read_type=0;
                                read_name=1;
                            }

                            else if (line[i] == ' ' && counter==0)
                            {
                                read_name=0;
                                read_pw=1;
                                counter++;

                            }
                            else if(line[i]==',')
                            {
                                read_pw=0;
                                read_real_name=1;


                            }
                            else if(line[i]==' ')
                            {
                                counter++;
                            }



                            else if(read_type)
                            {
                                strncat(type,&line[i],1);
                            }
                            else if(read_name)
                            {
                                strncat(username_from_file,&line[i],1);
                            }
                            else if(read_pw)
                            {
                                strncat(password_from_file,&line[i],1);
                            }
                            else if(read_real_name)
                            {
                                strncat(real_name,&line[i],1);
                            }

                        }

                    }

                }
                else
                {
                    continue;
                }
                    break;

        }
        fclose(fp);
        int len = strlen(real_name);
        int pos = 0;
        int num_uppercase = 0;
        for (int i = 0; i < len; i++)
        {
            if (isupper(real_name[i]))
                num_uppercase++;
            if (num_uppercase == 2)
            {
                pos = i;
                break;
            }
        }
        if (num_uppercase == 2)
        {
            for (int i = len; i >= pos; i--)
                real_name[i + 1] = real_name[i];
            real_name[pos] = ' ';
        }

        if(strcmp(type,"Admin")==0)
        {
                int x;
                char *username_from;
                char *number_of_login;
                FILE *fp_login_n=fopen("n_login.txt","r+");
                char line_for_login[100];
                while(fgets(line_for_login,sizeof(line_for_login),fp_login_n)!=NULL)
                {
                     line_for_login[strcspn(line_for_login,"\n")]=0;
                    if(strstr(line_for_login,username)!=NULL)
                    {
                        username_from=strtok(line_for_login," ");
                        number_of_login=strtok(NULL," ");

                    }

                }
                fclose(fp_login_n);
                sscanf(number_of_login,"%d",&x);

            if(strcmp(username_from_file,username)==0 && strcmp(password_from_file,password)==0 && (strlen(username_from_file)==strlen(username)) && (strlen(password_from_file)==strlen(password)))
            {

                clrscr();


                if(strcmp(username_from,"admin")==0 && x==0)
                {
                    //Resetovanje admin naloga.
                    x++;
                    char new_username[50];
                    char new_password[50];
                    printf("Resetovanje admin naloga!\n\n");
                    printf("Enter your username: ");
                    scanf("%s",new_username);
                    printf("Enter your password: ");
                    scanf("%s",new_password);
                    ftruncate((fileno(fp),0));
                    FILE *fp = fopen("users.txt", "w");
                    fseek(fp,0,SEEK_END);
                    fprintf(fp,"Admin:%s %s\n",new_username,new_password);
                    FILE *pom=fopen("pom.txt","a+");
                    fprintf(pom,"%s %d\n",new_username,x);
                    fflush(fp);
                    fflush(pom);
                    clrscr();
                    fclose(fp);
                    fclose(pom);
                    remove("n_login.txt");
                    rename("pom.txt","n_login.txt");
                    main();

                }
                else
                {
                    function_n_login_admin(username,real_name);
                }
            }
            else
            {
                printf("\nNe mozete se prijaviti, jer nemate nalog!\n\n");
                Exit();
            }
        }

        else if(strcmp(type, "Korisnik") == 0 && (strlen(username_from_file)==strlen(username)) && (strlen(password_from_file)==strlen(password)))
        {
            clrscr();
            funtion_n_login_user(username,real_name,username_from_file);
        }

        else if(strcmp(type, "Klijent") == 0 && (strlen(username_from_file)==strlen(username)) && (strlen(password_from_file)==strlen(password)))
        {
            clrscr();
            function_n_login_client(username,real_name);
        }

        else
        {
            char exit[50];
            printf("\nNe mozete se login jer nemate nalog!\n\n");
            Exit();
        }

        }
    }

    else if (choice == 2)
    {
        clrscr();
        int cho;

        printf("**************************************************************\n\n");
        printf("    Welcome to Registration of EventTickets application!\n\n");
        printf("**************************************************************\n\n");

        char user_name[50];
        char user_pw[50];
        char real_name[50];
        char real_surname[50];
        char email[50];
        char check_pw[50];
        //Moguće dodati još podataka
        do{
            printf("Unesite korisnicko ime: ");
            scanf("%s", user_name);
        } while(check_if_username_exist(user_name) == 1);

        printf("Unesite lozinku: ");
            scanf("%s", user_pw);
        printf("Unesite vase ime: ");
            scanf("%s", real_name);
        printf("Unesite prezime: ");
            scanf("%s", real_surname);
        printf("Unesite email: ");
            scanf("%s", email);
        printf("Potvrdite lozinku: ");
            scanf("%s", check_pw);
        if(strcmp(user_pw, check_pw) != 0)
        {
            do
            {
                printf("\nNetocna potvrda lozinke!  ");
                printf("Pokusajte ponovo potvrditi: ");
                scanf("%s", check_pw);
            } while(strcmp(user_pw, check_pw) != 0);

            FILE *fp = fopen("users.txt", "a");
            FILE *fp_login_n=fopen("n_login.txt","a+");
            fseek(fp,0,SEEK_END);
            fprintf(fp,"Korisnik:%s %s,%s %s %s\n",user_name,user_pw,real_name,real_surname,email);
            fprintf(fp_login_n,"%s %d\n",user_name,0);
            fflush(fp); fclose(fp);
            fclose(fp_login_n);

            FILE *fpCredits = fopen("credits.txt", "a");
            fprintf(fpCredits,"%s\n%d\n\n", user_name, 300);
            fflush(fpCredits); fclose(fpCredits);

            printf("\nUspjesno ste se registrovali!\n\n");
            Exit();
        }

        else if(strcmp(user_pw, check_pw)==0)
        {
            char exit[50];
            FILE *fp = fopen("users.txt", "a");
            FILE *fp_login_n=fopen("n_login.txt","a+");
            printf("\nUspjesno ste se registrovali!\n\n");
            fseek(fp,0,SEEK_END);
            fprintf(fp,"Korisnik:%s %s,%s %s %s\n",user_name,user_pw,real_name,real_surname,email);
            fprintf(fp_login_n,"%s %d\n",user_name,0);
            fflush(fp);
            fclose(fp);
            fclose(fp_login_n);
            FILE *fpCredits = fopen("credits.txt", "a");
            fprintf(fpCredits,"%s\n%d\n\n", user_name, 300);
            fflush(fpCredits); fclose(fpCredits);

            Exit();
        }

    }
    else if(choice == 3)
    {
        printf("\n\nThank you for using EventTickets, goodbye!\n\n");
    }

 return 0;

}




