# CantineWebAPI

Ce WebAPI Cantine permet de gérer la facturation d'un repas de cantine pour un utilisateur. Chaque utilisateur dispose d'un budget qu'il peut recharger. Lorsqu'il passe à la caisse, le coût total de son plateau est calculé en fonction des produits qu'il a sélectionnés. Des réductions peuvent s'appliquer en foncion de son status dans l'entreprise ou si un menu spécifique a été pris.

## Initialisation de la base de données
Ce projet nécessite une initialisation de la base de données initiale, contenant trois tables :
- Clients
- ClientCategories
- Products
  
Pour cela, il est demandé de lancer la commande suivante dans le **Package Manager Console** du projet **Cantine.DataAccess** :

```update-database```


Une fois la base de données installée, celle-ci contient par défault un utilisateur nommé **Michel Blanc**, **stagiaire** de l'entreprise et disposant d'un budget initial de **100€** sur son compte.

## Fonctionnaliés principales
### 1. Budget: Controller et Service

Le service **Budget** permet de recharger le budget d'un utilisateur via une requête **POST**. L'URL de l'API est : `https://localhost:7238/api/Budget` 

Cette requête doit inclure dans le corps de la requête :
- clientId : L'identifiant du client.
- amount : La somme à recharger sur le budget du client.


Exemple de requête pour Michel Blanc : 

```
{
 "clientId": "3720DA0A-9109-48C5-89AA-ACC7B7684294",
 "amount": 15
}
```

![image](https://github.com/user-attachments/assets/be16d5cc-658d-4461-82d4-f843ff15dd68)
![image](https://github.com/user-attachments/assets/7f5e4f7b-d7fa-485b-9bd7-1eb37906760b)


### 2. Ticket : Controller et Service

Le service **Ticket** permet gérer la transaction du plateau repas en calculant le coût du plateau, en appliquant des réducions éventuelles et en débiant le compte du client. L'URL de l'API est :  `https://localhost:7238/api/Ticket/order`

Cette requête **POST** doit inclure dans le coprs de la requête :
- ClientId : L'identifiant du client.
- Products : La liste de produits sélectionnés parmi les suivants :
  - Boisson
  - Fromage
  - Pain
  - Petite Salade Bar
  - Grand Salade Bar
  - Portion de fruit
  - Entrée
  - Plat
  - Dessert
 
Lors de la transaction, le service Ticket calcule le coût total du plateau avec le coût de chaque produit. Si un ou plusieurs menus ont été pris, une déduction est faite pour qu'ils ne coutent que 10€. Une autre réduction est également appliquée en fonction du status du client dans l'entreprise.
Enfin le cout final du ticket est déduit du budget du client si celui-ci a assez d'argent sur son compte.


Si la transaction est effectuée correctement, un **ticket** (objet json) est retourné. Il contient :
- ClientId : L'identifiant du client.
- Products : Les détails des produits sélectionnés.
- TotalToPay : Le coût total du plateau facturé.


Exemple de requête pour une commande :
![image](https://github.com/user-attachments/assets/457d1eaa-ef12-42fd-9c50-eaab084f33e3)
![image](https://github.com/user-attachments/assets/c8dc2284-1f18-48c1-9a31-0df26aae9882)
Dans l'exemple ci-dessus, le client est un stagiaire de l'entreprise donc son plateau ne lui coûte que 10,4€ - 10€ = 0,4€.






