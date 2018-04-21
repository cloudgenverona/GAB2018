# Global Azure Bootcamp 2018 - Service Fabric
Lo scopo di questo repository è quello di valutare l'Actor Model di Service Fabric.
In questo semplice esempio utilizzeremo diversi attori:
1. Banca di Zio Paperone. L'attore è preposto a depositare e prelevare soldi da parte e lo notifica direttamente ai due attori protagonisti.
2. Zio paperone. Riceve i soldi dalla sua banca
3. Banda bassotti. Ruba i soldi dalla banca di Zio paperone.
4. Qui. Riceve un terzo dell'eredità di Zio paperone. Memorizza i soldi in uno stato persistente e replicato.
5. Quo. Riceve un terzo dell'eredità di Zio paperone. Memorizza i soldi in uno stato persistente, in memoria e replicato.
6. Qua. Riceve un terzo dell'eredità di Zio paperone. Memorizza i soldi in uno stato non persistente.

La situazione iniziale è la seguente:
![Situazione iniziale](https://github.com/andreatosato/PaperonDePaperonFabric/raw/master/images/SituazioneIniziale.png)

Ogni servizio Actor Service viene replicato su 3 istanza mentre il portale web viene istanziato in una sola istanza.

![Dashboard](https://github.com/andreatosato/PaperonDePaperonFabric/raw/master/images/Dashboard.png)

L'attore Qua è replicato 3 nodi e il primario, ossia quello istanziato e che risponde alle richieste, è presente sul nodo 4.

![Qua nodi](https://github.com/andreatosato/PaperonDePaperonFabric/raw/master/images/Nodi_Qua.png)

Riavviando il nodo 4, in cui sono presenti altri servizi, riavviamo anche l'istanza primaria di Qua.

![Riavvio Qua](https://github.com/andreatosato/PaperonDePaperonFabric/raw/master/images/RestartQua.png)

Dopo il riavvio del nodo Qua, Service Fabric cambia il nodo principale dell'attore Qua e lo sposta sul nodo 2.

![Nodi dopo il riavvio](https://github.com/andreatosato/PaperonDePaperonFabric/raw/master/images/Nodi_Riavvio_Qua.png)

La situazione finale, dopo lo spegnimento del nodo 4, ci fa capire la persistenza dello stato dell'attore QUA.

![Situazione Finale](https://github.com/andreatosato/PaperonDePaperonFabric/raw/master/images/DopoRestart.png)