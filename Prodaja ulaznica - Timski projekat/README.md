# Sistem za Prodaju Ulaznica

Ovaj projekat implementira jednostavan sistem za prodaju ulaznica koji omogućava različitim vrstama korisnika da upravljaju svojim nalozima i događajima. Sistem čuva podatke o registrovanim korisnicima i događajima u datoteci unutar aplikacije.

## Opis

Svi korisnici moraju imati otvoren nalog, koji se sastoji od imena, prezimena, korisničkog imena i lozinke. Registracija na sistem je otvorena samo za kreiranje običnih korisničkih naloga. Kada se korisnici prijave, mogu koristiti različite opcije u zavisnosti od vrste naloga koju imaju. Postoje tri vrste korisničkih naloga: 

- **Obični korisnički**: Standardni korisnici koji mogu pristupiti osnovnim funkcionalnostima sistema.
- **Administratorski**: Administratori koji mogu upravljati klijentskim i korisničkim nalozima (aktivacija, suspendovanje, brisanje, poništavanje šifre).
- **Klijentski**: Klijentski nalozi mogu kreirati događaje za koje se prodaju ulaznice.

## Funkcionalnosti

- **Kreiranje događaja**: Klijenti mogu kreirati događaje unoseći sve potrebne podatke (naziv, datum, vreme, cena ulaznice, da li se kupuje na ime ili ne).
- **Prodaja ulaznica**: Svaka ulaznica ima jedinstvenu šifru, naziv događaja, datum, vreme, iznos i ime korisnika koji je kupio kartu.
- **Pregled prodatih ulaznica**: Klijenti mogu pregledati prodate ulaznice za svaki događaj i dobiti izvještaje o prodaji u određenom periodu (po datumu održavanja događaja) u .txt formatu.
- **Poništavanje ulaznica**: Korisnici mogu poništiti prethodno kupljenu ulaznicu.
- **Upravljanje kreditima**: Korisnici na svom nalogu imaju ukupan iznos kredita sa kojim obavljaju kupovinu. Napomena: Kupovina kredita se ne obavlja u ovom sistemu.
- **Sigurnost naloga**: Sistem dužan je da korisnici menjaju lozinku za pristup nakon svake `n` prijave na sistem, pri čemu se `n` definiše u konfiguraciji na nivou cijelog sistema.
- **Upravljanje fajlovima**: Svi direktorijumi i fajlovi koje sistem koristi čuvaju se u folderu na računaru, čija se putanja čuva u konfiguracionom fajlu.

## Napomena

Ovaj projekat je prvi timski projekat rađen u C jeziku bez ikakve modularizacije, te je kod neuredan i možda nije najbolje predstaviti ga kao primer kvaliteta rada u CV-u. Ipak, predstavlja korak ka razumevanju rada u timu i osnove upravljanja korisničkim sistemima.
