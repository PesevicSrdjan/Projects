# Kriptografska Aplikacija

Ova aplikacija simulira rad jednostavnih kriptografskih algoritama: Rail Fence, Myszkowski i Playfair. Aplikacija omogućava korisnicima da se registruju, prijave i enkriptuju proizvoljno unesen tekst koristeći odabrani algoritam.

## Opis

Korisnici se prvo moraju registrovati unosom korisničkog imena, lozinke i dodatnih podataka. Prilikom registracije automatski se generiše digitalni sertifikat i par RSA ključeva. Nakon uspješne registracije, korisnici se mogu prijaviti unosom sertifikata, korisničkog imena i lozinke. Nakon prijave, korisnici mogu birati između dostupnih algoritama i uneti tekst za enkripciju (do 100 karaktera).

## Funkcionalnosti

- **Registrovanje korisnika**: Unos korisničkog imena i lozinke uz generisanje digitalnog sertifikata i RSA ključeva.
- **Prijavljivanje korisnika**: Korisnici se prijavljuju putem digitalnog sertifikata i lozinke.
- **Enkripcija**: Simulacija enkripcije unesenog teksta koristeći Rail Fence, Myszkowski i Playfair algoritme.
- **Istorija simulacija**: Čuvanje rezultata simulacija u tekstualnim datotekama (TEKST | ALGORITAM | KLJUČ | ŠIFRAT).
- **Zaštita podataka**: Čuvanje tajnosti i integriteta korisničkih datoteka, sa detekcijom neovlaštenih izmena.
