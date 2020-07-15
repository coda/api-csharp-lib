# IO.Swagger.Api.ColumnsApi

All URIs are relative to *https://coda.io/apis/v1*

Method | HTTP request | Description
------------- | ------------- | -------------
[**GetColumn**](ColumnsApi.md#getcolumn) | **GET** /docs/{docId}/tables/{tableIdOrName}/columns/{columnIdOrName} | Get a column
[**ListColumns**](ColumnsApi.md#listcolumns) | **GET** /docs/{docId}/tables/{tableIdOrName}/columns | List columns

<a name="getcolumn"></a>
# **GetColumn**
> ColumnDetail GetColumn (string docId, string tableIdOrName, string columnIdOrName)

Get a column

Returns details about a column in a table.

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class GetColumnExample
    {
        public void main()
        {

            var apiInstance = new ColumnsApi();
            var docId = docId_example;  // string | ID of the doc.
            var tableIdOrName = tableIdOrName_example;  // string | ID or name of the table. Names are discouraged because they're easily prone to being changed by users. If you're using a name, be sure to URI-encode it.
            var columnIdOrName = columnIdOrName_example;  // string | ID or name of the column. Names are discouraged because they're easily prone to being changed by users. If you're using a name, be sure to URI-encode it.

            try
            {
                // Get a column
                ColumnDetail result = apiInstance.GetColumn(docId, tableIdOrName, columnIdOrName);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ColumnsApi.GetColumn: " + e.Message );
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
 **columnIdOrName** | **string**| ID or name of the column. Names are discouraged because they&#x27;re easily prone to being changed by users. If you&#x27;re using a name, be sure to URI-encode it. | 

### Return type

[**ColumnDetail**](ColumnDetail.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="listcolumns"></a>
# **ListColumns**
> ColumnList ListColumns (string docId, string tableIdOrName, int? limit = null, string pageToken = null, bool? visibleOnly = null)

List columns

Returns a list of columns in a table.

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class ListColumnsExample
    {
        public void main()
        {

            var apiInstance = new ColumnsApi();
            var docId = docId_example;  // string | ID of the doc.
            var tableIdOrName = tableIdOrName_example;  // string | ID or name of the table. Names are discouraged because they're easily prone to being changed by users. If you're using a name, be sure to URI-encode it.
            var limit = 56;  // int? | Maximum number of results to return in this query. (optional) 
            var pageToken = pageToken_example;  // string | An opaque token used to fetch the next page of results. (optional) 
            var visibleOnly = true;  // bool? | If true, returns only visible columns for the table. (optional) 

            try
            {
                // List columns
                ColumnList result = apiInstance.ListColumns(docId, tableIdOrName, limit, pageToken, visibleOnly);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ColumnsApi.ListColumns: " + e.Message );
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
 **limit** | **int?**| Maximum number of results to return in this query. | [optional] 
 **pageToken** | **string**| An opaque token used to fetch the next page of results. | [optional] 
 **visibleOnly** | **bool?**| If true, returns only visible columns for the table. | [optional] 

### Return type

[**ColumnList**](ColumnList.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
