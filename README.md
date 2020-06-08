## Objective
Improve a poorly written project. Apply all aspects of good software engineering (including but not limited to design, reliability, readability, extensibility, quality) and make it #beautiful.

## Improvements made
* Database connection string must not be within source code but in the configuration e.g. web.config
    * Helpers.cs deleted
    * Web.Config connection string added
* Fixed connection string for App_Data located attached database
* Namespaces must not contain _ underscore
    * Renamed refactor_me to Product.WebApi
* Every class must be placed in own file
    * ProductModel.cs, ProductOptionModel.cs, ProductListModel.cs, OptionListModel.cs created
* Single resposibility principle applied = do not place database operation into the model objects
    * Products.cs split into:
        * Model classes ProductModel, ProductOptionModel, ProductListModel, OptionListModel
        * Database repository classes ProductRepository, ProductOptionRepository
* Values must be passed as parameters to SQL query to avoid SQL Injection attack
* Class naming must clearly indicate the purpose = renamed classes
* Constructor must only initialize class properties not calling database operation
* Received values must be always validated
    * Added data annotations to ProductModel and ProductOptionModel class
* Project methods changed to asynchonous to improve performance and not block threads
* When delete a product the related options must be deleted too
* Web API Http Status Codes should be as follows:
    * 200 for successful GET
    * 201 for successful POST or PUT
    * 400 for not valid request
    * 404 for missing record

## Future improvements recommended
* Database layer recommend to put in own library project to separate from "presentation" layer
* Need to add Unit Testing
* Use SQL Server pro production environment
* Consider using MongoDB as a storage for product with options model
