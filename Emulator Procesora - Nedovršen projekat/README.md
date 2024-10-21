# Emulator za Sopstvenu Arhitekturu Procesora

Ovaj projekat implementira emulator za sopstvenu arhitekturu procesora, koristeći principe objektno-orijentisanog programiranja (OOP).

## Opis

Programerski model procesora obuhvata:

- **Četiri 64-bitna registra opšte namjene**
- **Programski brojač**
- **64-bitni adresni prostor**, gdje sadržaj svake memorijske adrese ima dužinu od 1 bajta.

### Instrukcijski skup

Emulator podržava sljedeće instrukcije:

1. **Osnovne aritmetičke operacije**:
   - ADD (sabiranje)
   - SUB (oduzimanje)
   - MUL (množenje)
   - DIV (deljenje)

2. **Osnovne bitske logičke operacije**:
   - AND
   - OR
   - NOT
   - XOR

3. **Instrukcije za transfer podataka**:
   - MOV (premjesti), uz podršku za direktno i indirektno adresiranje.

4. **Instrukcije za bezuslovno i uslovno grananje**:
   - JMP (grananje)
   - JE (grananje ako je jednako)
   - JNE (grananje ako nije jednako)
   - JGE (grananje ako je veće ili jednako)
   - JL (grananje ako je manje)
   - CMP (uporedi), uz podršku za direktno i indirektno grananje.

5. **I/O rutine**:
   - Učitavanje znaka sa tastature u registar
   - Ispis znaka iz registra na ekran

6. **Instrukcija za zaustavljanje rada procesora**:
   - HALT

## Simulacija Mehanizma Keš Memorije

Projekat takođe uključuje simulaciju mehanizma keš memorije u emulatoru. Pri pokretanju emulatora, korisnici mogu konfigurisati:

- Broj keš nivoa
- Veličine keša po nivoima
- Asocijativnost keša po nivoima
- Veličinu keš linije

### Dodatne funkcionalnosti

- Omogućava uvid u procenat memorijskih pristupa koji rezultuju keš promašajem.
- Specifikacija algoritma za zamjenu keš linija, uključujući:
  - LRU (Least Recently Used) algoritam
  - Optimalni (Bélády) algoritam

Implementirane funkcionalnosti su demonstrirane primjerima.

## Napomena

Ovaj projekat je nedovršen, mehanizam keš memorije nije implementiran. Iako predstavlja dobar korak ka razumijevanju arhitekture procesora i mehanizama keširanja.

