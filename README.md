# BookCatalog
A feladatotot a kijelölt frontend/backend technológiák közül Blazor WASM és ASP.NET Core segítségével valósítottam meg.
# Setup 

Adatbázisnak lokális MSSQL adatbázist használtam, ennek elérése a WebAPI projekt appsettings.json fájlában a Default Connection tulajdonsága alatt található, persze a neve (Initial Catalog, alapértelemezetten "BC_v1") átírható, a DbInitializer osztálynak le kéne futtatnia minden migration-t és inicializálnia azt adatbázist pár értékkel.
Letölthető innen [https://learn.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb?view=sql-server-ver17](https://learn.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb?view=sql-server-ver17), valamint része Visual Studo ASP.NET workload-nak.

WebAPI és Blazor egymás közötti kapcsolata szintén a saját appsettings fájlukban van beállítva szimplán az egymásra utaló localhost tulajdonságokkal.

Futtatni az alkalmazást a WebAPI és Blazor projektek közös indításával lehet, ekkor egy Swagger oldal is megnyílik egyszerűbb tesztelés kedvéért.

Induláskor a többi végpont elérése előtt be kell jelentkezni vagy regisztrálni, alapvetően elérhető felhasználó adatai : felhasználónév "TestUser", jelszó "test@123" .

Pár funkció elérése ami talán nem egyértelmű azonnal:
- A könyvek listájának rendezése a táblázat oszolopainak fejlécére kattintva történik
- Keresni substring alapján lehet a megadott opciók közül (cím, író név, műfaj név)
- Egy könyv címére kattintva lehet elérni a részletes nézetét, ahol értékelni lehet, és ahol megjelennek az értékelések is
