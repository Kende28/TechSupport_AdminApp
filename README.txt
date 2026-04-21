  TECHSUPPORT ADMIN APP - TELEPÍTÉSI ÉS INDÍTÁSI ÚTMUTATÓ

------------------------------------------------------------------------[MAGYAR]----------------------------------------------------------------------

1. ELŐFELTÉTELEK
  • Visual Studio 2022 vagy újabb (Community verzió is megfelelő)
  • .NET 6.0 SDK vagy újabb
  • MySQL szerver (a backend kezel majd az adatbázist)
  • Backend szerver: https://github.com/Atmerium/TechSupport_Backend
  • Node.js és npm (ha a backendhez szükséges)

2. PROJEKT LETÖLTÉSE GITHUB-RÓL
  a) Git böngészőben navigálj ide:
     https://github.com/Atmerium/TechSupport_AdminApp

  b) Kattints a "Code" zöld gombra, kopipálj az HTTPS URL-t

  c) Nyiss meg egy terminálablakot a kód helyének közelében és futtasd:
     git clone https://github.com/Atmerium/TechSupport_AdminApp.git
     cd TechSupport_AdminApp-master

3. BACKEND BEÁLLÍTÁSA
  a) Telepítsd a backend szervert (lásd: TechSupport_Backend README)
  
  b) Győződj meg, hogy a backend fut a 3000-es porton:
     http://localhost:3000
  
  c) Ellenőrizd, hogy az auth/login és /parts végpontok működnek:
     POST http://localhost:3000/auth/login
     GET http://localhost:3000/parts

4. PROJEKT MEGNYITÁSA A VISUAL STUDIÓBAN
  a) Nyisd meg a Visual Studiót
  
  b) File → Open → Project/Solution
  
  c) Navigálj az letöltött mappához és válaszd ki:
     TechSupport.AdminApp.sln

  d) Kattints "Open"

5. MEGOLDÁS FORDÍTÁSA
  a) Build → Build Solution (vagy Ctrl+Shift+B)
  
  b) Várj, amíg a fordítás befejeződik
  
  c) Ha hibák vannak, töltsd le az összes NuGet csomagot:
     Tools → NuGet Package Manager → Package Manager Console
     >> Update-Package

6. ALKALMAZÁS INDÍTÁSA (DEBUG MÓD)
  a) Nyomj F5 vagy kattints Debug → Start Debugging
  
  b) Az alkalmazás megnyitja a LoginWindow ablakot
  
  c) Bejelentkezési adatok:
     • E-mail/Felhasználónév: (a backend MySQL adatbázisban tárolt adatok)
     • Jelszó: (a backend MySQL adatbázisban tárolt jelszó)
  
  d) Sikeres bejelentkezés után megnyílik a MainWindow

7. HASZNÁLAT
  a) Alkatrészek (Parts) listázása: Frissítés gomb
  
  b) Alkatrész létrehozása:
     - Töltsd ki a megnevezést és leírást
     - Jelöld be a "Látható" opcionálisan
     - Kattints "Létrehozás"
  
  c) Alkatrész módosítása:
     - Válassz egyet a listából
     - Módosítsd az adatokat
     - Kattints "Módosítás"
  
  d) Alkatrész törlése:
     - Válassz egyet a listából
     - Kattints "Törlés"
  
  e) Kijelentkezés:
     - Kattints a "Kijelentkezés" gombra

8. BACKEND ÖSSZEKÖTÉS
  Az alkalmazás az AppConfig.cs fájlban definiált backend URL-t használja:
  http://localhost:3000
  
  Ha máshol futna a backend, módosítsd az AppConfig.cs-ben:
  public static string ApiBaseUrl { get; } = "http://localhost:xxxx/";

9. HIBAELHÁRÍTÁS
  a) "Backend nem elérhető":
     - Ellenőrizd, hogy a Node.js backend szerver fut-e
     - Nyisd meg böngészőben: http://localhost:3000/api
  
  b) "Bejelentkezési hiba":
     - Győződj meg, hogy a MySQL adatbázis és a backend fut
     - Ellenőrizd a felhasználó adatait az adatbázisban
  
  c) "Fordítási hibák":
     - Töltsd le az összes NuGet csomagot
     - Töröld az obj és bin mappákat
     - Fordítsd újra


------------------------------------------------------------------------[ENGLISH]----------------------------------------------------------------------

1. REQUIREMENTS
  • Visual Studio 2022 or newer (Community edition is fine)
  • .NET 6.0 SDK or newer
  • MySQL Server (backend will manage the database)
  • Backend Server: https://github.com/Atmerium/TechSupport_Backend
  • Node.js and npm (if needed for backend)

2. DOWNLOADING THE PROJECT FROM GITHUB
  a) Navigate to:
     https://github.com/Atmerium/TechSupport_AdminApp

  b) Click the green "Code" button, copy the HTTPS URL

  c) Open terminal in your code location and run:
     git clone https://github.com/Atmerium/TechSupport_AdminApp.git
     cd TechSupport_AdminApp-master

3. SETTING UP THE BACKEND
  a) Install the backend server (see TechSupport_Backend README)
  
  b) Make sure the backend is running on port 3000:
     http://localhost:3000
  
  c) Verify that auth/login and /parts endpoints work:
     POST http://localhost:3000/auth/login
     GET http://localhost:3000/parts

4. OPENING THE PROJECT IN VISUAL STUDIO
  a) Open Visual Studio
  
  b) File → Open → Project/Solution
  
  c) Navigate to the downloaded folder and select:
     TechSupport.AdminApp.sln

  d) Click "Open"

5. BUILDING THE SOLUTION
  a) Build → Build Solution (or Ctrl+Shift+B)
  
  b) Wait for the build to complete
  
  c) If there are errors, restore all NuGet packages:
     Tools → NuGet Package Manager → Package Manager Console
     >> Update-Package

6. RUNNING THE APPLICATION (DEBUG MODE)
  a) Press F5 or click Debug → Start Debugging
  
  b) The application opens the LoginWindow
  
  c) Login credentials:
     • Email/Username: (stored in backend MySQL database)
     • Password: (stored in backend MySQL database)
  
  d) After successful login, MainWindow opens

7. USAGE
  a) List components: Click "Frissítés" (Refresh) button
  
  b) Create a component:
     - Fill in the name and description
     - Optionally check "Látható" (Visible)
     - Click "Létrehozás" (Create)
  
  c) Edit a component:
     - Select one from the list
     - Modify the details
     - Click "Módosítás" (Update)
  
  d) Delete a component:
     - Select one from the list
     - Click "Törlés" (Delete)
  
  e) Logout:
     - Click the "Kijelentkezés" (Logout) button

8. BACKEND CONNECTION
  The application uses the backend URL defined in AppConfig.cs:
  http://localhost:3000
  
  If your backend runs elsewhere, modify AppConfig.cs:
  public static string ApiBaseUrl { get; } = "http://localhost:xxxx/";

9. TROUBLESHOOTING
  a) "Backend is not available":
     - Check if the Node.js backend server is running
     - Open in browser: http://localhost:3000/api
  
  b) "Login error":
     - Ensure MySQL database and backend are running
     - Verify user credentials in the database
  
  c) "Build errors":
     - Restore all NuGet packages
     - Delete obj and bin folders
     - Rebuild the solution

Version: 1.0
Last Updated: 2026-04-20
Repository: https://github.com/Atmerium/TechSupport_AdminApp
Backend: https://github.com/Atmerium/TechSupport_Backend
Frontend: https://github.com/Atmerium/TechSupport_Frontend
