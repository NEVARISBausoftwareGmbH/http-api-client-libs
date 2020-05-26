# http-api-client-libs für NEVARIS Build 2020.2

## Nevaris.Build.ClientApi

Diese .NET-Bibliothek ermöglicht einen typsicheren Zugriff auf die RESTful API von NEVARIS Build. Sie ist auch als [nuget-Paket](https://www.nuget.org/packages/Nevaris.Build.ClientApi/) verfügbar.

## Beispielcode ##

Beispielcode zur Verwendung der Nevaris.Build.ClientApi findet sich unter [http-api-demo-clients](https://github.com/NEVARISBausoftwareGmbH/http-api-demo-clients).

## Voraussetzungen ##

Um die RESTful API von NEVARIS Build nutzen zu können, muss auf einem geeigneten Server der NEVARIS Businessdienst installiert werden. Dazu muss im Setup-Programm zunächst _Benutzerdefiniert_ ausgewählt werden:

![SetupBenutzerdefiniert](Docs/SetupBenutzerdefiniert.png)

und anschließend in der Installationsauswahl _Businessdienst_ und _RESTful API_ angehakt werden:

![SetupAuswahl](Docs/SetupAuswahl.png)

Der Port, über den die RESTful API erreichbar ist, ist konfigurierbar (der Standardwert ist 8500):

![SetupBusinessdienstKonfiguration](Docs/SetupBusinessdienstKonfiguration.png)

Nach erfolgreicher Installation wird der Dienstprozess _NEVARIS Build Businessdienst_ gestartet. Dieser stellt die RESTful API sowie eine HTML-basierte Dokumentation bereit, die über die URL **http://\<servername\>:8500/api-docs** erreichbar ist (falls 8500 als Portnummer gewählt wurde):

![SwaggerDoku](Docs/SwaggerDoku.png)

Die Doku enthält auch einen Link auf eine swagger.json-Datei, die eine formale Beschreibung der API enthält. Daraus lässt sich mit geeigneten Tools auch automatisiert ein Client-Proxy erzeugen, falls dies erwünscht ist.
