TECHSUPPORT ADMIN APP - TELEPÍTÉSI ÉS INDÍTÁSI ÚTMUTATÓ

------------------------------------------------------------------------[MAGYAR]----------------------------------------------------------------------

1. ELŐFELTÉTELEK
  • Visual Studio 2022 vagy újabb (Community verzió is megfelelő)
  • .NET 8.0 SDK (projekt target: net8.0-windows)
  • MySQL szerver (a backend használja az adatbázist)
  • Node.js és npm (a backendhez és frontend dev szerverhez)

2. PROJEKT LETÖLTÉSE
  a) Klónozd a repót:
     git clone <repo-url>
     cd <repo-folder>

3. ADATBÁZIS LÉTREHOZÁSA
  a) Hozd létre az adatbázist és táblákat a mellékelt SQL script futtatásával:
     mysql -u root -p < TechSupport.Backend/init_db.sql
  b) A script létrehozza a tech_support_db adatbázist és a szükséges táblákat (users, token, parts), valamint egy teszt admin felhasználót: admin / admin123

4. BACKEND BEÁLLÍTÁS
  a) Másold a mintafájlt és szerkeszd a kapcsolat beállításait:
     cd TechSupport.Backend
     copy .env.example .env    (Windows)
     cp .env.example .env      (Linux/macOS)
  b) Állítsd be a .env fájlban a DB_HOST, DB_USER, DB_PASSWORD, DB_NAME értékeket.
  c) Telepítsd a függőségeket és indítsd el a backendet:
     npm install
     npm start
  d) Ellenőrizd: http://localhost:3000/parts

5. FRONTEND (DEV) - opcionális
  A mappában található egy Vite + React placeholder:
     cd TechSupport.Frontend
     npm install
     npm run dev
  A dev szerver alapértelmezett portja: 5173. A vite config proxyzza az /auth és /parts kéréseket a helyi backend-re.

6. WPF KLIENS (AdminApp)
  a) Nyisd meg a TechSupport.AdminApp.sln fájlt Visual Studio-ban (2022+).
  b) Build → Build Solution (az MSBuild target megkísérli telepíteni a backend npm csomagjait, ha szükséges).
  c) Futtatás (F5) — megnyílik a LoginWindow.
  d) Bejelentkezés: admin / admin123 (init_db.sql-ben létrehozott teszt felhasználó)


7. JÓTANÁCSOK ÉS HIBAELHÁRÍTÁS
  • Ha a kliens "Backend nem elérhető" hibát mutat, ellenőrizd, hogy a Node backend fut és a .env-ben megadott DB elérhető.
  • Ha a DB csatlakozás hibát ad, ellenőrizd a .env beállításokat és futtasd újra az init_db.sql-t.
  • Ha a WPF projekt target framework eltérő (.NET SDK hiányzik), telepítsd a .NET 8.0 SDK-t vagy állítsd vissza a projekt TargetFramework-et a telepített SDK-nak megfelelően.

------------------------------------------------------------------------[ÁTMENETI FÁJLOK, AMIKET ÁT KELL TENNI]-------------------------------------------

Ha a backendet, frontendet és az adatbázist máshová mozgatod a gépeden, másold át az alábbi ideiglenes fájlokat és végezd el a leírt manuális lépéseket, hogy az AdminApp csatlakozni tudjon:

- TechSupport.Backend/init_db.sql
  - Importáld a MySQL szerverbe a sémát és a teszt admin felhasználót:
    mysql -u root -p < init_db.sql

- TechSupport.Backend/.env (vagy .env.example)
  - Tartalmazza az adatbázis csatlakozási beállításokat (DB_HOST, DB_USER, DB_PASSWORD, DB_NAME) és a szerver portját.
  - Helyezd el ezt a fájlt a backend új helyén és indítsd újra a backendet.

- TechSupport.Backend/package.json
  - Ha a backendet forrásból futtatod, tartsd meg a package.json-t a függőségek telepítéséhez (npm install).

- TechSupport.Frontend/package.json és vite config (vite.config.ts)
  - Ha a frontend fejlesztői szervert futtatod, frissítsd a proxy-t vagy az API alap URL-jét, hogy a mozgatott backendre mutasson.

AdminApp csatlakoztatásának manuális lépései:
1) Szerkeszd a TechSupport.AdminApp/AppConfig.cs fájlt -> ApiBaseUrl a backend alap URL-jére ( '/'-al végződjön), pl. "http://localhost:3000/" vagy "https://api.example.com/".
2) Biztosítsd, hogy a backend engedélyezze a CORS-t a frontend origin számára (ha a frontend külön van), és fogadja a kéréseket az AdminApp hostjától.
3) Importáld az init_db.sql-t egy elérhető adatbázisba és frissítsd a backend .env-jét ezekkel a DB hitelesítő adatokkal.
4) Indítsd el a backend és frontend szolgáltatásokat az új helyükön.
5) Buildeld és futtasd az AdminApp-ot (Visual Studio). Jelentkezz be normálisan, vagy teszteléshez állíts be érvényes tokent az AppConfig.BearerToken-be.

Tesztelési példák:
- Bejelentkezés (visszaad token-t):
  curl -s -X POST "{API_BASE}auth/login" -H "Content-Type: application/json" -d '{"username":"admin","password":"admin123"}'
- Alkatrészek listázása tokennel:
  curl -s -H "Authorization: Bearer TOKEN" "{API_BASE}parts"


------------------------------------------------------------------------[ENGLISH]----------------------------------------------------------------------

1. REQUIREMENTS
  • Visual Studio 2022 or newer
  • .NET 8.0 SDK (project targets net8.0-windows)
  • MySQL Server
  • Node.js and npm

2. CLONE THE REPOSITORY
  git clone <repo-url>
  cd <repo-folder>

3. CREATE THE DATABASE
  Run the included SQL script to create the database and tables:
  mysql -u root -p < TechSupport.Backend/init_db.sql
  The script creates tech_support_db and tables (users, token, parts) and a sample admin user (admin / admin123).

4. BACKEND SETUP
  cd TechSupport.Backend
  copy .env.example .env   (Windows)  OR  cp .env.example .env (Linux/macOS)
  Edit .env with your DB connection values (DB_HOST, DB_USER, DB_PASSWORD, DB_NAME)
  npm install
  npm start
  Check: http://localhost:3000/parts

5. FRONTEND (DEV) - optional
  cd TechSupport.Frontend
  npm install
  npm run dev
  The Vite dev server proxies /auth and /parts to the local backend.

6. WPF CLIENT (AdminApp)
  Open TechSupport.AdminApp.sln in Visual Studio.
  Build → Build Solution
  Run (F5). Login window opens.
  Login with admin / admin123

7. NOTES & TROUBLESHOOTING
  • Ensure TechSupport.Backend/.env points to a reachable MySQL instance.
  • If backend cannot access DB, check credentials and run init_db.sql manually.
  • MSBuild targets try to run npm install and start the backend before debugging; you can still run backend manually.

Version: 1.1
Last Updated: 2026-04-20
Repository: local workspace


------------------------------------------------------------------------[TEMPORARY FILES TO MOVE]----------------------------------------------------

If you moved the backend, frontend and database elsewhere on your machine, copy/move these temporary files and perform the manual steps below so the AdminApp can connect:

- TechSupport.Backend/init_db.sql
  - Import into your MySQL server to create the schema and a test admin user:
    mysql -u root -p < init_db.sql

- TechSupport.Backend/.env (or .env.example)
  - Contains DB connection settings (DB_HOST, DB_USER, DB_PASSWORD, DB_NAME) and server port.
  - Place this file next to the backend service in its new location and restart the backend.

- TechSupport.Backend/package.json
  - If you run the backend from source, keep package.json to install dependencies (npm install).

- TechSupport.Frontend/package.json and vite config (vite.config.ts)
  - If running the frontend dev server, update its proxy or base API URL to point to the moved backend.

Manual steps for AdminApp to connect:
1) Edit TechSupport.AdminApp/AppConfig.cs -> ApiBaseUrl to the backend base URL (must end with '/'), e.g. "http://localhost:3000/" or "https://api.example.com/".
2) Ensure the backend exposes CORS for the frontend origin (if frontend is separate) and accepts requests from the AdminApp host.
3) Import init_db.sql into a reachable DB instance and update the backend .env with those DB credentials.
4) Start backend and frontend services in their new locations.
5) Build and run the AdminApp (Visual Studio). Login normally or paste a valid token into AppConfig.BearerToken for testing.

Testing examples:
- Login (returns token):
  curl -s -X POST "{API_BASE}auth/login" -H "Content-Type: application/json" -d '{"username":"admin","password":"admin123"}'
- List parts with token:
  curl -s -H "Authorization: Bearer TOKEN" "{API_BASE}parts"

