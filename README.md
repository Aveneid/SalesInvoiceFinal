# SalesInvoiceFinal
Niniejsze repozytorium zawiera kod aplikacji napisanej w ramach pracy inżynierskiej "System ewidencji usług i sprzedaży w działalności gospodarczej" na Uniwersytecie Pedagogicznym im. KEN w Krakowie. 
Aplikacja została napisana w języku VB.NET, środowisko Visual Studio 2015.

## Wymagane oprogramowanie do uruchomienia aplikacji
Minimalne wymagania systemowe:
- system Windows (Xp+) lub inny system wspierający poniższe biblioteki i binaria,
- .Net Framework 4.6+,
- SQL Server (R) Compact Edition 4.0+ [Download](https://www.microsoft.com/en-us/download/details.aspx?id=30709)

## Wymagane pakiety NuGet
Do poprawnej kompilacji aplikacji wymagane są następujące pakiety:
- https://www.nuget.org/packages/FreeSpire.PDF/,
- https://www.nuget.org/packages/EntityFramework/

## Kompilacja
Aby skompilować aplikację na swojej maszynie i korzystać z jej całej funkcjonalności należy uzupełnić klucz API w pliku 
[DownloadDataFromCEIDG.vb](https://github.com/Aveneid/SalesInvoiceFinal/blob/master/Client/DownloadDataFromCEIDG.vb#L20), klucz jest możliwy do uzyskania po [rejestacji](https://datastore.ceidg.gov.pl/) i jego 
[wygenerowaniu](https://datastore.ceidg.gov.pl/CEIDG.DataStore/CEIDG.Public.UI/User/UserEdit.aspx), dokumentacja API dostępna jest [tutaj](https://datastore.ceidg.gov.pl/CEIDG.DataStore/Styles/Regulations/API_Datastore_20190314.pdf).

## Rozwijanie aplikacji
Każda osoba zainteresowana może rozwijać, zmieniać i edytować aplikację pod swoje indywidualne potrzeby, zachowując przy tym warunki licencji.
W tym repozytorium znajdować się będzie wersja aplikacji, która została przedłożona w celach dyplomowania. Natomiast w repozytorium [SalesInvoice](https://github.com/Aveneid/SalesInvoice) znajdować się będzie aktualna i rozwijana wersja aplikacji. Wszystkie znalezione błędy proszę zgłaszać przy pomocy zakładki "Issues" w w/w repozytorium w celu ich weryfikacji i eliminacji.

## Licencja
GNU GPL
