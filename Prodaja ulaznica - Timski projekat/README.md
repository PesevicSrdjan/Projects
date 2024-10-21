# Sistem za Prodaju Ulaznica

Ovaj projekat implementira jednostavan sistem za prodaju ulaznica koji omogućava različitim vrstama korisnika da upravljaju svojim nalozima. Sistem čuva podatke o registrovanim korisnicima u datoteci unutar aplikacije.

## Opis

Svi korisnici moraju imati otvoren nalog, koji se sastoji od imena, prezimena, korisničkog imena i lozinke. Registracija na sistem je otvorena samo za kreiranje običnih korisničkih naloga. Kada se korisnici prijave, mogu koristiti različite opcije u zavisnosti od vrste naloga koju imaju. Postoje tri vrste korisničkih naloga: 

- **Obični korisnički**: Standardni korisnici koji mogu pristupiti osnovnim funkcionalnostima sistema.
- **Administratorski**: Administratori koji mogu upravljati klijentskim i korisničkim nalozima (aktivacija, suspendovanje, brisanje, poništavanje šifre).
- **Klijentski**: Klijentske naloge mogu kreirati samo administratori.

Prvi podrazumevani administratorski nalog dolazi u sklopu instalacije i mora se promeniti nakon prve prijave (korisničko ime: `admin`, lozinka: `admin`).

## Funkcionalnosti

- Registracija korisnika sa osnovnim podacima.
- Prijava korisnika na osnovu vrste naloga.
- Administratorske mogućnosti za upravljanje korisnicima.
- Blokiranje klijentskih događaja.

## Napomena

Ovaj projekat je prvi timski projekat rađen u C jeziku bez ikakve modularizacije, te je kod neuredan i možda nije najbolje predstaviti ga kao primer kvaliteta rada. Ipak, predstavlja korak ka razumevanju rada u timu i osnove upravljanja korisničkim sistemima.
