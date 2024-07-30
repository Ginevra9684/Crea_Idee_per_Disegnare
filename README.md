# CREA IDEE PER DISEGNARE

- Creare un'app che dia consigli su cosa progettare sia in 2d che 3d ed eventualmente dica quali oggetti/soggetti prendere come reference

## Target

Chiunque voglia disegnare o creare un progetto 3D ma non ha una consegna precisa o non ha delle idee da cui partire

# Funzionalità Base

- L'app chiede all'utente se vuole creare un'ambientazione, un soggetto o entrambi.
- Per l'`ambientazione` sceglierà, in maniera `random` e da un `file` esterno, un `luogo specifico`.
- Per il `soggetto` chiederà all'utente se ha delle `preferenze` (umano, animale, creatura).
- Se l'utente non ha preferenze, il computer chiederà se l'utente desidera `un solo` soggetto o `una coppia` di soggetti (umano + animale/creatura)
- Se la scelta è `creatura` l'app chiede se si vuole una `creatura mitologica` o se si vuole creare una `propria invenzione`
- Se l'utente vuole creare una propria invenzione gli verrà fornita una `lista` di `animali` con un quantitativo di elementi tra 2 e 5
- L'app chiede se l'tente necessita di un `tema`
- L'app chiede se si necessita di una `tecnica` di disegno

# PROGETTAZIONE 

<details> 

<summary> Steps </summary>


- [X] Creare un file Json contenente le ambientazioni
- [X] Creare un file Json contenente vari animali
- [X] Creare un file Json contenente creature mitologiche
- [X] Creare un file Json contenente vari temi
- [X] Creare un file Json contenente le possibili tecniche

- [X] Scrivere il menu principale di scelta (ambientazione, soggetto, entrambi)
- [X] Creare una funzione per poter estrarre i vari elementi dai file json
- [X] - estrarre il luogo dal file Json

- [x] Aggiungere il sottomenu del soggetto (umano, animale, creatura, nessuna preferenza)
- [ ] - scelta uno o due soggetti se non si ha nessuna preferenza
- [ ] - - estrazione da file Json

- [ ] - estrarre tema
- [ ] - estrarre tecnica

- [ ] Chiusura del programma
- [ ] Gestione delle eccezioni

</details>

<details>
<summary> schema </summary>

```mermaid
stateDiagram-v2
    state if_state <<choice>>
    state if_state2 <<choice>>
    state if_state3 <<choice>>
    state if_state4 <<choice>>
    state if_state5 <<choice>>
    state if_state6 <<choice>>
    CREA_IDEE --> Scelta
    Scelta --> Soggetto?
    Scelta --> Ambientazione_e_Soggetto?
    Scelta --> Ambientazione?
    Ambientazione? --> Luogo_Specifico 
    Soggetto? --> Preferenza?\n(umano/animale/creatura)
    Preferenza?\n(umano/animale/creatura) --> if_state
    if_state --> Scelta_Manuale\nun_solo_soggetto : if risposta = si
    Scelta_Manuale\nun_solo_soggetto --> if_state2
    if_state2 --> Tema? : if scelta != creatura
    if_state2 --> Creatura_Mitologica_o\nPropria_Creazione? : if scelta = creatura
    Creatura_Mitologica_o\nPropria_Creazione? --> if_state3
    if_state3 --> Creatura_Mitologica\nRandom : if scelta = 1
    Creatura_Mitologica\nRandom --> Tema?
    if_state3 --> Quantità_Animali_per\nCreazione? : if scelta = 2
    Quantità_Animali_per\nCreazione? --> Lista_Animali_Random
    Lista_Animali_Random --> Tema?
    if_state --> Scelta_Soggetto_Unico_o\nDoppio_Soggetto : if risposta = no
    Scelta_Soggetto_Unico_o\nDoppio_Soggetto --> if_state4
    if_state4 --> Soggetto_Random : if risposta = 1
    if_state4 --> Soggetto_Umano_+\nSoggetto_Random : if risposta = 2
    Ambientazione_e_Soggetto? --> Luogo_Specifico
    Luogo_Specifico --> Tema? : if ambientazione
    Luogo_Specifico --> Preferenza?\n(umano/animale/creatura) : if ambientazione e soggetto
    Soggetto_Random --> Tema? : if Random != creatura
    Soggetto_Random --> Creatura_Mitologica_o\nPropria_Creazione? : if Random = creatura
    Soggetto_Umano_+\nSoggetto_Random --> Creatura_Mitologica_o\nPropria_Creazione? : if Random = creatura
    Soggetto_Umano_+\nSoggetto_Random --> Tema? : if Random != creatura
    Tema?--> if_state5
    if_state5 --> Tema_Random : if risposta = si
    Tema_Random --> Tecnica?
    if_state5 --> Tecnica? : if risposta = no
    Tecnica? --> if_state6
    if_state6 --> Tecnica_Random : if risposta = si
    if_state6 --> FINE : if risposta = no
    Tecnica_Random --> FINE
```

</details>

<deyails>
<summary>Commits</summary>

```bash
git add --all
git commit -m "Progettazione ReadMe"
git push -u origin main

git add --all
git commit -m "Aggiunti 5 file json per estrapolare elementi"
git push -u origin main

git add --all
git commit -m "Menu principale + funzione per estrarre elementi da file json"
git push -u origin main

```

</details>