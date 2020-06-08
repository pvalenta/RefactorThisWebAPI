## Objective
Improve a poorly written project. Apply all aspects of good software engineering (including but not limited to design, reliability, readability, extensibility, quality) and make it #beautiful.

## Improvements made

### Code improvements
* Database connection string must not be within source code but in the configuration e.g. web.config
    * Helpers.cs deleted
    * Web.Config connection string added
* Fixed connection string for App_Data located attached database
* Namespaces must not contain _ underscore
    * Renamed refactor_me to WebApi
* Every class must be placed in own file
    * Products.cs deleted
    * ProductModel.cs, ProductOptionModel.cs, ProductListModel.cs, OptionListModel.cs created
* When delete a product the related options must be deleted too
* Project methods changed to asynchonous to improve performance and not block threads
* Web API Http Status Codes should be as follows:
    * 200 for successful GET
    * 201 for successful POST or PUT
    * 400 for not valid request
    * 404 for missing record

### Security Issues
* Values must be passed as parameters to SQL query to avoid SQL Injection attack
* Received values must be always validated
    * Added data annotations to ProductModel and ProductOptionModel class

### Design Patterns 
* Constructor must only initialize class properties not calling database operation
* Class naming must clearly indicate the purpose = renamed classes
* Single resposibility principle applied = do not place database operation into the model objects
    * Contracts: contain interfaces for Models and Repositories (abstraction layer)
    * Entities: contain domain models ProductModel, ProductOptionModel with abstraction layer for database (IDataReader)
    * LocalSqlRepository: this library implement LocalSQL database repository following Contracts and Models
    * classes ProductModel, ProductOptionModel, ProductListModel, OptionListModel
    * Database repository classes ProductRepository, ProductOptionRepository
* Added Dependency Injection with Unity Container

## Future improvements recommended
* Need to add Unit Testing
* Should add more data validation e.g. product price and delivery price
* Use SQL Server pro production environment
* Consider using MongoDB as a storage for product with options model
