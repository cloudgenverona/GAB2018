# Introduzione
Il codice sorgente caricato in questo repository è stato utilizzato e mostrato durante la sessione "Introduzione a Visual Studio App Center" nell'evento Global Azure Bootcamp 2018 organizzato a Verona da Cloudgen. Tutte le altre sessioni dell'evento sono disponibili gratuitamente su [GitHub](https://github.com/CloudGenVR/GAB2018).

Per motivi di tempo, tutta la demo è incentrata sullo sviluppo dell'applicazione Android, anche se l'architettura presente è di una applicazione Xamarin.Forms. L'implementazione per iOS è incompleta al momento del caricamento del materiale, ma sono ben accette Pull Request per integrare la parte mancante.

## Creazione di un account
La creazione di un account di Visual Studio App Center è gratuita e può essere effettuata da <https://appcenter.ms/>.  
La registrazione al servizio è obbligatoria per testare il funzionamento dell'applicazione.

Una volta ottenuto l'account, registrare una nuova applicazione come spiegato nella [documentazione](https://docs.microsoft.com/en-us/appcenter/dashboard/creating-and-managing-apps "creating and managing apps in App Center").

Nella pagina *Getting started* è possibile recuperare il token da sostituire all'*app-center-id* descritto nella [*MainActivity*](https://github.com/matteotumiati/gab-verona-2018-app-center/blob/b7b871852e0cacf0e022a486a1453c54cf7f09b3/Droid/MainActivity.cs#L40):

```
AppCenter.Start("********-****-****-****-**********", typeof(Analytics), typeof(Crashes));
```

Per recuperare il [*sender-id*](https://github.com/matteotumiati/gab-verona-2018-app-center/blob/b7b871852e0cacf0e022a486a1453c54cf7f09b3/Droid/MainActivity.cs#L32) per testare il funzionamento delle push notification è necessario seguire la documentazione di [Firebase per Xamarin.Android](https://docs.microsoft.com/en-us/appcenter/sdk/push/xamarin-android).

## Remarks
L'uso dei servizi di Visual Studio App Center è completamente gratuito se mantenuto entro i limiti esposti nella pagina relativa al [*pricing*](https://docs.microsoft.com/en-us/appcenter/general/pricing).  
L'unico servizio a pagamento utilizzato in questa demo è quello dei test della UI, *App Center Test*, ma è possibile attivare la trial gratuita di 30 giorni per testarne tutte le funzionalità.
