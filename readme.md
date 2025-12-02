# StockAPI

StockAPI est une API REST développée avec **ASP.NET Core** permettant de gérer des produits, des catégories et les mouvements de stock. Elle s'appuie sur Entity Framework Core pour l'accès aux données.

## 📁 Structure du projet

```
StockAPI/
 ├── Controllers/
 │    ├── ProductsController.cs
 │    ├── CategoriesController.cs
 │    └── StockMovementsController.cs
 ├── Models/
 │    ├── Product.cs
 │    ├── Category.cs
 │    └── StockMovement.cs
 ├── Services/
 │    ├── IProductService.cs
 │    └── ProductService.cs
 ├── Data/
 │    └── AppDbContext.cs
 ├── Program.cs
 └── appsettings.json
```

## 🚀 Fonctionnalités principales

* Gestion des **produits** (CRUD)
* Gestion des **catégories** (CRUD)
* Gestion des **mouvements de stock** (entrée/sortie)
* Service métier pour centraliser la logique (ProductService)
* Base de données via Entity Framework Core (AppDbContext)

## 🏗️ Pré-requis

* .NET 6 ou supérieur
* SQL Server ou autre base compatible EF Core

## ⚙️ Configuration

L’application fonctionne avec **SQL Server**. Assurez-vous que SQL Server ou SQL Server Express est installé.

### 🔧 Modifier le fichier de configuration (appsettings.json)

Mettez à jour la chaîne de connexion SQL Server selon votre environnement :
La connexion se configure dans **appsettings.json** :

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=StockDB;User Id=xxxx; Password=xxxx;Encrypt=true;TrustServerCertificate=True;"
  }
}
```

## ▶️ Lancement du projet

### 1. Restaurer les dépendances

```
dotnet restore
```

### 2. Appliquer les migrations EF Core

```
dotnet ef database update
```

### 3. Lancer l’API

```
dotnet run
```

L’API sera disponible sur :

```
https://localhost:5001
http://localhost:5000
```

## 📚 Points d'entrée API

### Produits

| Méthode | Route              | Description          |
| ------- | ------------------ | -------------------- |
| GET     | /api/products      | Liste des produits   |
| GET     | /api/products/{id} | Détails d'un produit |
| POST    | /api/products      | Ajouter un produit   |
| PUT     | /api/products/{id} | Modifier un produit  |
| DELETE  | /api/products/{id} | Supprimer un produit |

### Catégories

| Méthode | Route           | Description           |
| ------- | --------------- | --------------------- |
| GET     | /api/categories | Liste des catégories  |
| POST    | /api/categories | Ajouter une catégorie |

### Mouvements de stock

| Méthode | Route               | Description                          |
| ------- | ------------------- | ------------------------------------ |
| POST    | /api/stockmovements | Ajouter un mouvement (entrée/sortie) |
| GET     | /api/stockmovements | Historique des mouvements            |

## 📦 Modèles principaux

### Product

* Id
* Name
* CategoryId
* Quantity
* Price

### Category

* Id
* Name

### StockMovement

* Id
* ProductId
* Quantity
* MovementType (IN/OUT)
* Date

## 🧩 Services

### IProductService & ProductService

Gère :

* La création/modification des produits
* La mise à jour des quantités après mouvement

## 🗄️ AppDbContext

Définit les DbSet :

```csharp
public DbSet<Product> Products { get; set; }
public DbSet<Category> Categories { get; set; }
public DbSet<StockMovement> StockMovements { get; set; }
```

## 📜 Licence

Projet interne — utilisation libre en entreprise.

## 🗃️ Exemple de configuration SQL Server (compatible SQL Server 2008)

SQL Server 2008 ne supporte **pas** les paramètres modernes comme `Encrypt` ou `TrustServerCertificate`. Utilisez une chaîne de connexion simplifiée comme ci-dessous.
Pour SQL Server 2008 local :

```
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=StockDB;User Id=sa;Password=VotreMotDePasse;"
}
```

Pour SQL Server 2008 distant :

```
"ConnectionStrings": {
  "DefaultConnection": "Server=192.168.1.10;Database=StockDB;User Id=stock_user;Password=MotDePasseFort;"
}
```

"ConnectionStrings": {
"DefaultConnection": "Server=localhost\SQLEXPRESS;Database=StockDB;User Id=sa;Password=VotreMotDePasse;Encrypt=False;"
}

```

Pour SQL Server distant :
```

"ConnectionStrings": {
"DefaultConnection": "Server=192.168.1.10;Database=StockDB;User Id=stock_user;Password=MotDePasseFort;Encrypt=True;TrustServerCertificate=True;"
}
