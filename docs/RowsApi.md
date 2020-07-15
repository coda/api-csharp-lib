# IO.Swagger.Api.RowsApi

All URIs are relative to *https://coda.io/apis/v1*

Method | HTTP request | Description
------------- | ------------- | -------------
[**DeleteRow**](RowsApi.md#deleterow) | **DELETE** /docs/{docId}/tables/{tableIdOrName}/rows/{rowIdOrName} | Delete row
[**DeleteRows**](RowsApi.md#deleterows) | **DELETE** /docs/{docId}/tables/{tableIdOrName}/rows | Delete multiple rows
[**GetRow**](RowsApi.md#getrow) | **GET** /docs/{docId}/tables/{tableIdOrName}/rows/{rowIdOrName} | Get a row
[**ListRows**](RowsApi.md#listrows) | **GET** /docs/{docId}/tables/{tableIdOrName}/rows | List table rows
[**PushButton**](RowsApi.md#pushbutton) | **POST** /docs/{docId}/tables/{tableIdOrName}/rows/{rowIdOrName}/buttons/{columnIdOrName} | Push a button
[**UpdateRow**](RowsApi.md#updaterow) | **PUT** /docs/{docId}/tables/{tableIdOrName}/rows/{rowIdOrName} | Update row
[**UpsertRows**](RowsApi.md#upsertrows) | **POST** /docs/{docId}/tables/{tableIdOrName}/rows | Insert/upsert rows

<a name="deleterow"></a>
# **DeleteRow**
> RowDeleteResult DeleteRow (string docId, string tableIdOrName, string rowIdOrName)

Delete row

Deletes the specified row from the table or view. This endpoint will always return a 202, so long as the row exists and is accessible (and the update is structurally valid). Row deletions are generally processed within several seconds. When deleting using a name as opposed to an ID, an arbitrary row will be removed. 

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class DeleteRowExample
    {
        public void main()
        {

            var apiInstance = new RowsApi();
            var docId = docId_example;  // string | ID of the doc.
            var tableIdOrName = tableIdOrName_example;  // string | ID or name of the table. Names are discouraged because they're easily prone to being changed by users. If you're using a name, be sure to URI-encode it.
            var rowIdOrName = rowIdOrName_example;  // string | ID or name of the row. Names are discouraged because they're easily prone to being changed by users. If you're using a name, be sure to URI-encode it. If there are multiple rows with the same value in the identifying column, an arbitrary one will be selected. 

            try
            {
                // Delete row
                RowDeleteResult result = apiInstance.DeleteRow(docId, tableIdOrName, rowIdOrName);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RowsApi.DeleteRow: " + e.Message );
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
 **rowIdOrName** | **string**| ID or name of the row. Names are discouraged because they&#x27;re easily prone to being changed by users. If you&#x27;re using a name, be sure to URI-encode it. If there are multiple rows with the same value in the identifying column, an arbitrary one will be selected.  | 

### Return type

[**RowDeleteResult**](RowDeleteResult.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="deleterows"></a>
# **DeleteRows**
> RowsDeleteResult DeleteRows (RowsDelete body, string docId, string tableIdOrName)

Delete multiple rows

Deletes the specified rows from the table or view. This endpoint will always return a 202. Row deletions are generally processed within several seconds. 

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class DeleteRowsExample
    {
        public void main()
        {

            var apiInstance = new RowsApi();
            var body = new RowsDelete(); // RowsDelete | Rows to delete.
            var docId = docId_example;  // string | ID of the doc.
            var tableIdOrName = tableIdOrName_example;  // string | ID or name of the table. Names are discouraged because they're easily prone to being changed by users. If you're using a name, be sure to URI-encode it.

            try
            {
                // Delete multiple rows
                RowsDeleteResult result = apiInstance.DeleteRows(body, docId, tableIdOrName);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RowsApi.DeleteRows: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **body** | [**RowsDelete**](RowsDelete.md)| Rows to delete. | 
 **docId** | **string**| ID of the doc. | 
 **tableIdOrName** | **string**| ID or name of the table. Names are discouraged because they&#x27;re easily prone to being changed by users. If you&#x27;re using a name, be sure to URI-encode it. | 

### Return type

[**RowsDeleteResult**](RowsDeleteResult.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="getrow"></a>
# **GetRow**
> RowDetail GetRow (string docId, string tableIdOrName, string rowIdOrName, bool? useColumnNames = null, ValueFormat valueFormat = null)

Get a row

Returns details about a row in a table.

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class GetRowExample
    {
        public void main()
        {

            var apiInstance = new RowsApi();
            var docId = docId_example;  // string | ID of the doc.
            var tableIdOrName = tableIdOrName_example;  // string | ID or name of the table. Names are discouraged because they're easily prone to being changed by users. If you're using a name, be sure to URI-encode it.
            var rowIdOrName = rowIdOrName_example;  // string | ID or name of the row. Names are discouraged because they're easily prone to being changed by users. If you're using a name, be sure to URI-encode it. If there are multiple rows with the same value in the identifying column, an arbitrary one will be selected. 
            var useColumnNames = true;  // bool? | Use column names instead of column IDs in the returned output. This is generally discouraged as it is fragile. If columns are renamed, code using original names may throw errors.  (optional) 
            var valueFormat = new ValueFormat(); // ValueFormat | The format that cell values are returned as. (optional) 

            try
            {
                // Get a row
                RowDetail result = apiInstance.GetRow(docId, tableIdOrName, rowIdOrName, useColumnNames, valueFormat);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RowsApi.GetRow: " + e.Message );
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
 **rowIdOrName** | **string**| ID or name of the row. Names are discouraged because they&#x27;re easily prone to being changed by users. If you&#x27;re using a name, be sure to URI-encode it. If there are multiple rows with the same value in the identifying column, an arbitrary one will be selected.  | 
 **useColumnNames** | **bool?**| Use column names instead of column IDs in the returned output. This is generally discouraged as it is fragile. If columns are renamed, code using original names may throw errors.  | [optional] 
 **valueFormat** | [**ValueFormat**](ValueFormat.md)| The format that cell values are returned as. | [optional] 

### Return type

[**RowDetail**](RowDetail.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="listrows"></a>
# **ListRows**
> RowList ListRows (string docId, string tableIdOrName, string query = null, RowsSortBy sortBy = null, bool? useColumnNames = null, ValueFormat valueFormat = null, bool? visibleOnly = null, int? limit = null, string pageToken = null)

List table rows

Returns a list of rows in a table.

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class ListRowsExample
    {
        public void main()
        {

            var apiInstance = new RowsApi();
            var docId = docId_example;  // string | ID of the doc.
            var tableIdOrName = tableIdOrName_example;  // string | ID or name of the table. Names are discouraged because they're easily prone to being changed by users. If you're using a name, be sure to URI-encode it.
            var query = query_example;  // string | Query used to filter returned rows, specified as `<column_id_or_name>:<value>`. If you'd like to use a column name instead of an ID, you must quote it (e.g., `\"My Column\":123`). Also note that `value` is a JSON value; if you'd like to use a string, you must surround it in quotes (e.g., `\"groceries\"`).  (optional) 
            var sortBy = new RowsSortBy(); // RowsSortBy | Specifies the sort order of the rows returned. If left unspecified, rows are returned by creation time ascending. (optional) 
            var useColumnNames = true;  // bool? | Use column names instead of column IDs in the returned output. This is generally discouraged as it is fragile. If columns are renamed, code using original names may throw errors.  (optional) 
            var valueFormat = new ValueFormat(); // ValueFormat | The format that cell values are returned as. (optional) 
            var visibleOnly = true;  // bool? | If true, returns only visible rows and columns for the table. (optional) 
            var limit = 56;  // int? | Maximum number of results to return in this query. (optional) 
            var pageToken = pageToken_example;  // string | An opaque token used to fetch the next page of results. (optional) 

            try
            {
                // List table rows
                RowList result = apiInstance.ListRows(docId, tableIdOrName, query, sortBy, useColumnNames, valueFormat, visibleOnly, limit, pageToken);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RowsApi.ListRows: " + e.Message );
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
 **query** | **string**| Query used to filter returned rows, specified as &#x60;&lt;column_id_or_name&gt;:&lt;value&gt;&#x60;. If you&#x27;d like to use a column name instead of an ID, you must quote it (e.g., &#x60;\&quot;My Column\&quot;:123&#x60;). Also note that &#x60;value&#x60; is a JSON value; if you&#x27;d like to use a string, you must surround it in quotes (e.g., &#x60;\&quot;groceries\&quot;&#x60;).  | [optional] 
 **sortBy** | [**RowsSortBy**](RowsSortBy.md)| Specifies the sort order of the rows returned. If left unspecified, rows are returned by creation time ascending. | [optional] 
 **useColumnNames** | **bool?**| Use column names instead of column IDs in the returned output. This is generally discouraged as it is fragile. If columns are renamed, code using original names may throw errors.  | [optional] 
 **valueFormat** | [**ValueFormat**](ValueFormat.md)| The format that cell values are returned as. | [optional] 
 **visibleOnly** | **bool?**| If true, returns only visible rows and columns for the table. | [optional] 
 **limit** | **int?**| Maximum number of results to return in this query. | [optional] 
 **pageToken** | **string**| An opaque token used to fetch the next page of results. | [optional] 

### Return type

[**RowList**](RowList.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="pushbutton"></a>
# **PushButton**
> PushButtonResult PushButton (string docId, string tableIdOrName, string rowIdOrName, string columnIdOrName)

Push a button

Pushes a button on a row in a table. Authorization note: This action is available to API tokens that are authorized to write to the table. However, the underlying button can perform any action on the document, including writing to other tables and performing Pack actions. 

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class PushButtonExample
    {
        public void main()
        {

            var apiInstance = new RowsApi();
            var docId = docId_example;  // string | ID of the doc.
            var tableIdOrName = tableIdOrName_example;  // string | ID or name of the table. Names are discouraged because they're easily prone to being changed by users. If you're using a name, be sure to URI-encode it.
            var rowIdOrName = rowIdOrName_example;  // string | ID or name of the row. Names are discouraged because they're easily prone to being changed by users. If you're using a name, be sure to URI-encode it. If there are multiple rows with the same value in the identifying column, an arbitrary one will be selected. 
            var columnIdOrName = columnIdOrName_example;  // string | ID or name of the column. Names are discouraged because they're easily prone to being changed by users. If you're using a name, be sure to URI-encode it.

            try
            {
                // Push a button
                PushButtonResult result = apiInstance.PushButton(docId, tableIdOrName, rowIdOrName, columnIdOrName);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RowsApi.PushButton: " + e.Message );
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
 **rowIdOrName** | **string**| ID or name of the row. Names are discouraged because they&#x27;re easily prone to being changed by users. If you&#x27;re using a name, be sure to URI-encode it. If there are multiple rows with the same value in the identifying column, an arbitrary one will be selected.  | 
 **columnIdOrName** | **string**| ID or name of the column. Names are discouraged because they&#x27;re easily prone to being changed by users. If you&#x27;re using a name, be sure to URI-encode it. | 

### Return type

[**PushButtonResult**](PushButtonResult.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="updaterow"></a>
# **UpdateRow**
> RowUpdateResult UpdateRow (RowUpdate body, string docId, string tableIdOrName, string rowIdOrName, bool? disableParsing = null)

Update row

Updates the specified row in the table. This endpoint will always return a 202, so long as the row exists and is accessible (and the update is structurally valid). Row updates are generally processed within several seconds. When updating using a name as opposed to an ID, an arbitrary row will be affected. 

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class UpdateRowExample
    {
        public void main()
        {

            var apiInstance = new RowsApi();
            var body = new RowUpdate(); // RowUpdate | Row update.
            var docId = docId_example;  // string | ID of the doc.
            var tableIdOrName = tableIdOrName_example;  // string | ID or name of the table. Names are discouraged because they're easily prone to being changed by users. If you're using a name, be sure to URI-encode it.
            var rowIdOrName = rowIdOrName_example;  // string | ID or name of the row. Names are discouraged because they're easily prone to being changed by users. If you're using a name, be sure to URI-encode it. If there are multiple rows with the same value in the identifying column, an arbitrary one will be selected. 
            var disableParsing = true;  // bool? | If true, the API will not attempt to parse the data in any way. (optional) 

            try
            {
                // Update row
                RowUpdateResult result = apiInstance.UpdateRow(body, docId, tableIdOrName, rowIdOrName, disableParsing);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RowsApi.UpdateRow: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **body** | [**RowUpdate**](RowUpdate.md)| Row update. | 
 **docId** | **string**| ID of the doc. | 
 **tableIdOrName** | **string**| ID or name of the table. Names are discouraged because they&#x27;re easily prone to being changed by users. If you&#x27;re using a name, be sure to URI-encode it. | 
 **rowIdOrName** | **string**| ID or name of the row. Names are discouraged because they&#x27;re easily prone to being changed by users. If you&#x27;re using a name, be sure to URI-encode it. If there are multiple rows with the same value in the identifying column, an arbitrary one will be selected.  | 
 **disableParsing** | **bool?**| If true, the API will not attempt to parse the data in any way. | [optional] 

### Return type

[**RowUpdateResult**](RowUpdateResult.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="upsertrows"></a>
# **UpsertRows**
> RowsUpsertResult UpsertRows (RowsUpsert body, string docId, string tableIdOrName, bool? disableParsing = null)

Insert/upsert rows

Inserts rows into a table, optionally updating existing rows if any upsert key columns are provided. This endpoint will always return a 202, so long as the doc and table exist and are accessible (and the update is structurally valid). Row inserts/upserts are generally processed within several seconds. When upserting, if multiple rows match the specified key column(s), they will all be updated with the specified value. 

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class UpsertRowsExample
    {
        public void main()
        {

            var apiInstance = new RowsApi();
            var body = new RowsUpsert(); // RowsUpsert | Rows to insert or upsert.
            var docId = docId_example;  // string | ID of the doc.
            var tableIdOrName = tableIdOrName_example;  // string | ID or name of the table. Names are discouraged because they're easily prone to being changed by users. If you're using a name, be sure to URI-encode it.
            var disableParsing = true;  // bool? | If true, the API will not attempt to parse the data in any way. (optional) 

            try
            {
                // Insert/upsert rows
                RowsUpsertResult result = apiInstance.UpsertRows(body, docId, tableIdOrName, disableParsing);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RowsApi.UpsertRows: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **body** | [**RowsUpsert**](RowsUpsert.md)| Rows to insert or upsert. | 
 **docId** | **string**| ID of the doc. | 
 **tableIdOrName** | **string**| ID or name of the table. Names are discouraged because they&#x27;re easily prone to being changed by users. If you&#x27;re using a name, be sure to URI-encode it. | 
 **disableParsing** | **bool?**| If true, the API will not attempt to parse the data in any way. | [optional] 

### Return type

[**RowsUpsertResult**](RowsUpsertResult.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
