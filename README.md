# FundaSorterApi [WIP]

## General approach to this problem
I think the likelyhood solution is; choosing SQL based database for the following reasons:
- Easy to group by fields (by "MakelaarNaam")
- Easy to filter (by the ones that contain "tuin")
- Guaranteed redundancy (occurrence of the same entry can be easily handled)
- Handy Sorting features
- Having the right queries would minimize API functionality

This is why I didn't choose SQL solution because it's already a no-brainer solution.
I wanted to show an alternative solution that we can discuss its benefits and drawbacks.

## FundaSorterApi's approach to this problem
The current data storage solution is a cache database which is Redis

Reasons;
- Faster response time (especially on repeatedly requested data)
- Easier modeling since it's using general data structures
- Optimization of peak time queries

## Implementation Details
The current implementation contains 3 different controllers for this API
- EstateCollectorController
- ObsoleteEstateCollectorController
- DebugController

### EstateCollectorController
This controller has four endpoints
- #### HttpPost("RetrieveRealEstatesFromMessage")
   I have added this function to test it before consuming the FundaApi, it's allowing you to send the data in the message body
- #### HttpPost("RetrieveAllRealEstatesFromFunda")
   This method consumes from FundaApi and takes two parameters;
   - cityName  (eg. "amsterdam")
   - searchFor (eg. "tuin")

  It will provide the parameters to the HttpClient which will construct a URL string to consume and then start retrieving from FundaAPI
  There will be 600 ms of waiting time between each request to FundaAPI
  Then it will refine data into SortedHash in Redis. makelaarNaam will be key, number of elements related to that makelaar will be value
- #### [HttpPost("CacheInDataFromFunda-Experimental")]
  
  This method takes the same parameters as RetrieveAllRealEstatesFromFunda only difference, it stores all the data in the cache db for future use
  My ultimate goal was to refine data out of this data but there was not enough time. I just left it here to show you the thought process.
- #### [HttpGet("GetTopTenMakelaars")]
  
  This method will return the latest results of the RetrieveAllRealEstatesFromFunda method in descending order.

### ObsoleteEstateCollectorController
As stated in the name this controller is obsolete, I am fully aware that I shouldn't keep this in the repo.
Just to demonstrate the thought process.

### DebugController
This one is a direct backdoor to the data storage. I cannot guarantee its functionality. 
It's nice to have this controller during the development time to test other features.

## Other features
### CacheConnection
This file contains connection details and a connector to the Redis database

***Note: In a real-life scenario configuration should be retrieved from configuration files and controllers should access this via dependency injection.***

### PropertyCollectorClient
This file contains;
- HttpClient details
- Url builder
- Data fetcher
- 600 ms time delay between each request
- Json to model deserializer
  
***Note: In a real-life scenario configuration should be retrieved from configuration files and controllers should access this via dependency injection.***

### ObsoleteSortedLinkedList
As stated in the name this class is obsolete, I am fully aware that I shouldn't keep this in the repo.
My first thinking was to collect all the data sort it then store it in a DB. That's why I've started this implementation. After I've found that Redis provides
**SortedHash**, this class became obsolete.

## Testability
- Current implementation supports end-to-end to testing.
- Full coverage is not possible due to obsolete and unfinished methods
- Full coverage with unit test can be possible if connections is converted to interfaces
  This will support mocking the external connections.

## Improvements to make
- ***The most important and unfinished improvement is to consume the FundaAPI once and then use the cached data. Only then does this choice of data storage make sense***.
- All of the clients/connectors should collect connection strings from the configuration
- All of the clients/connectors should be included in connectors with dependency injection.
- Logging must be spread all over the API
- Null checking for all nullable references should be in place
- All endpoints should be configured to return correct status codes


## Usage
Using Visual Studio is highly recommended as it will allow Swagger to test this API.

    cd ./FundaSorterApi/
    docker compose up



