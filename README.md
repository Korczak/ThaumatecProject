# Thaumatec Project

Project that gives opportunity to steer 3D printer remotely. It was developed as part of the classes at the Wrocław University of Technology in cooperation with the Thaumatec company.

## Requirements
* npm
* nswag
* mongodb
* .net core 3.1

## Instalation
### Install server
1. Install mongodb
2. Run on visual studio `Thaumatec.Web`
3. Run client

### Install frontend
1. On `Thaumatec.Web/Frontend/` run `npm install` this will install all dependencies
2. On visual studio code run `Ctrl+Shift+P` - `task serve`, this will build client
3. After building client, it is reacheable as default on localhost:8080

### Genereta nswag client
1. Install nswag - run `npm install nswag -g` 
2. On main directory run `tp.bat client` to generate nswag client - it is located on `Thaumatec.Web/Frontend/src/api-clients/`
3. If errors occurs, add to environment variable path to nswag net core 3.1
   for example `C:\...\AppData\Roaming\npm\node_modules\nswag\bin\binaries\NetCore31` 
