# Word Counter
## Created by Olena Kuchko

### Description
A web application which allow user manage with stylists and clients in hair salon. User can add, update and delete stylists and clients. Also user is able to search  information about stylists.

### Installation
1. Download or clone the repository from https://github.com/LenaKuchko/HairSalon.git
2. Using PowerShell (for Windows), navigate to the directory in which you downloaded project.
3. To recreate a database use SSMS or another similar application.
4. In PowerShell run the next command: sqlcmd -S "(localdb)\mssqllocaldb" or sqlcmd -S (particular name of SQLServer instance on your PC). To recreate database tape in PowerShell:
  4.1. CRAETE TABLE stylist (id INT IDENTITY(1,1), name VARCHAR(50), rating INT); After this press Enter, then type GO; press Enter again.
  4.2. CRAETE TABLE clients (id INT IDENTITY(1,1), name VARCHAR(50), stylist_id INT); press Enter, then type GO; press Enter again.
6. In PowerShell then run dnu restore and dnx kestrel.
7. In your Web-browser enter - localhost:5004.
8. Using IDE of your choice to open code.

### Known Bugs.

| Behavior | Input | Output |
|----------|-------|--------|
|User add a stylist | "Jessica", 5 | App displays new stylist with name "Jessica" and rating 5 |  
|User can view information about stylist | Click on stylist's name | App displays information about stylist including all clients which belong to this stylist |
|User add a client | "Tom", "Jessica"| App displays confirmation and information about client that was just added |
|User is able to find stylist by name | "Jessica"| App displays information about stylist "Jessica" |
|User is able to update stylist profile. Change name "Jessssica" and (or) rating 3| "Jessica" and (or) 5 | App displays updated information "Jessica" and(or) 5 |
|User can update client. Change name "tomm" and (or) stylist "Rebecca" | "Tom" and (or) "Jessica" |App displays updated information "Tom" and (or) "Jessica" |
|User is able to delete client | Click on "delete client" button | App displays confirmation about deleting client|
|User is able to delete all stylists | Click on "delete all" button| App displays confirmation about deleting All STYLISTS and ALL CLIENTS|


#### License
This project is licensed under the MIT License.
Copyright (c) 2017  Olena Kuchko
