# IO.Swagger.Api.TablesApi

All URIs are relative to *https://coda.io/apis/v1*

Method | HTTP request | Description
------------- | ------------- | -------------
[**GetTable**](TablesApi.md#gettable) | **GET** /docs/{docId}/tables/{tableIdOrName} | Get a table
[**ListTables**](TablesApi.md#listtables) | **GET** /docs/{docId}/tables | List tables

<a name="gettable"></a>
# **GetTable**
> Table GetTable (string docId, string tableIdOrName)

Get a table

Returns details about a specific table or view.

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class GetTableExample
    {
        public void main()
        {

            var apiInstance = new TablesApi();
            var docId = docId_example;  // string | ID of the doc.
            var tableIdOrName = tableIdOrName_example;  // string | ID or name of the table. Names are discouraged because they're easily prone to being changed by users. If you're using a name, be sure to URI-encode it.

            try
            {
                // Get a table
                Table result = apiInstance.GetTable(docId, tableIdOrName);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling TablesApi.GetTable: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **docId** | **string**| ID of the doc. | 
 **tableIdOrName** | **string**| ID or name of the table. Names are discouraged because they&#x27;re easily prone to being changed by users. If you&#x27;re using a name, be sure to URI-encode it. | 

### Return type

[**Table**](Table.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="listtables"></a>
# **ListTables**
> TableList ListTables (string docId, int? limit = null, string pageToken = null, SortBy sortBy = null, List<TableType> tableTypes = null)

List tables

Returns a list of tables in a Coda doc.

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class ListTablesExample
    {
        public void main()
        {

            var apiInstance = new TablesApi();
            var docId = docId_example;  // string | ID of the doc.
            var limit = 56;  // int? | Maximum number of results to return in this query. (optional) 
            var pageToken = pageToken_example;  // string | An opaque token used to fetch the next page of results. (optional) 
            var sortBy = new SortBy(); // SortBy | Determines how to sort the given objects. (optional) 
            var tableTypes = new List<TableType>(); // List<TableType> | Comma-separated list of table types to include in results. If omitted, includes both tables and views. (optional) 

            try
            {
                // List tables
                TableList result = apiInstance.ListTables(docId, limit, pageToken, sortBy, tableTypes);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling TablesApi.ListTables: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **docId** | **string**| ID of the doc. | 
 **limit** | **int?**| Maximum number of results to return in this query. | [optional] 
 **pageToken** | **string**| An opaque token used to fetch the next page of results. | [optional] 
 **sortBy** | [**SortBy**](SortBy.md)| Determines how to sort the given objects. | [optional] 
 **tableTypes** | [**List&lt;TableType&gt;**](TableType.md)| Comma-separated list of table types to include in results. If omitted, includes both tables and views. | [optional] 

### Return type

[**TableList**](TableList.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
