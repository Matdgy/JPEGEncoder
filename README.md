# JPEG Encoder

### Áttekintés
Ez a projekt a JPEG kódolás részleges implementációját tartalmazza. A program egy bitmap képből állít elő JPEG kódolás során előforduló részeredményeket. Az implementáció a .NET Core 8.0 verziójával készült.

---

### Build
A solution a következő módon buildelhető:

`$ nuget restore JPEGEncoder.sln`
A System.Drawing.Common nuget package letöltése céljából.

`$ dotnet build JPEGEncoder.sln`
.NET SDK 8.0 verzó szükséges.

---

### Implementáció
A program képes bármilyen .bmp fájlt kezelni és megnyitni. A fájl olvasás után a képeket átalakítja **egykomponensű** egész szám alapú reprezentációkká három színcsatorna esetén is (Alpha csatorna elhagyásával). Az implementáció a kódolás során vegrehajtott lépéseket csak erre az egy csatornára végzi el, mely megfelelhet a **szürkeárnyalatú** (monokróm) képek során használt fényintezitásnak.

Megvalósított lépések:

- Beolvasás, szürkeárnyalati konverzó
- Kép kiegészítés (8x8 blokkokra bontás előfeltétele)
- Értékek 0-hoz való középpontosítása
- Képfüggvény blokkokra bontása
- Blokkok diszkrét koszinusz transzformációja
- DCT együtthatók kvantálása
- Cikk-cakk alakú kiolvasás, futamhossz kódolás

A Huffmann-kódolás illetve a JFIF fájlkezelés és szerializáció hiányzik a programból.
