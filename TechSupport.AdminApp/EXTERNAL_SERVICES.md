Csatlakozás külső backendhez

1) API base URL beállítása
- Nyisd meg: TechSupport.AdminApp/AppConfig.cs
- Módosítsd az ApiBaseUrl értékét a külső backend URL-jére (például: "https://api.example.com/"). A végződés legyen '/'.

2) Szükséges végpontok és formátumok
- POST {base}auth/login
  - Body: { "username": "...", "password": "..." }
  - Válasz: { "token": "...", "userName": "...", "userEmail": "..." }
- GET {base}parts
- GET {base}parts/{id}
- POST {base}parts (Authorization: Bearer TOKEN)
- PUT {base}parts/{id} (Authorization: Bearer TOKEN)
- DELETE {base}parts/{id} (Authorization: Bearer TOKEN)

3) Gyors példa curl-lel
- Bejelentkezés:
  curl -s -X POST "${API_BASE}auth/login" -H "Content-Type: application/json" -d '{"username":"admin","password":"pass"}'
- Lista lekérése tokennel:
  curl -s -H "Authorization: Bearer TOKEN" "${API_BASE}parts"

4) Hitelesítés kezelése az alkalmazásban
- A bejelentkezéskor visszakapott tokent az alkalmazás AppConfig.BearerToken-be menti.
- Ha manuálisan akarsz tokent beállítani: módosítsd AppConfig.BearerToken értékét.

Megjegyzés: A projektben már eltávolítottuk a helyi backend- és frontend fájlokat; az AdminApp külső szolgáltatásokhoz csatlakozik a fenti beállításokkal.
