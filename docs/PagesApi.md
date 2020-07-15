# IO.Swagger.Api.PagesApi

All URIs are relative to *https://coda.io/apis/v1*

Method | HTTP request | Description
------------- | ------------- | -------------
[**GetPage**](PagesApi.md#getpage) | **GET** /docs/{docId}/pages/{pageIdOrName} | Get a page
[**ListPages**](PagesApi.md#listpages) | **GET** /docs/{docId}/pages | List pages
[**UpdatePage**](PagesApi.md#updatepage) | **PUT** /docs/{docId}/pages/{pageIdOrName} | Update a page

<a name="getpage"></a>
# **GetPage**
> Page GetPage (string docId, string pageIdOrName)

Get a page

Returns details about a page.

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class GetPageExample
    {
        public void main()
        {

            var apiInstance = new PagesApi();
            var docId = docId_example;  // string | ID of the doc.
            var pageIdOrName = pageIdOrName_example;  // string | ID or name of the page. Names are discouraged because they're easily prone to being changed by users. If you're using a name, be sure to URI-encode it. If you provide a name and there are multiple pages with the same name, an arbitrary one will be selected. 

            try
            {
                // Get a page
                Page result = apiInstance.GetPage(docId, pageIdOrName);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PagesApi.GetPage: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **docId** | **string**| ID of the doc. | 
 **pageIdOrName** | **string**| ID or name of the page. Names are discouraged because they&#x27;re easily prone to being changed by users. If you&#x27;re using a name, be sure to URI-encode it. If you provide a name and there are multiple pages with the same name, an arbitrary one will be selected.  | 

### Return type

[**Page**](Page.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="listpages"></a>
# **ListPages**
> PageList ListPages (string docId, int? limit = null, string pageToken = null)

List pages

Returns a list of pages in a Coda doc.

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class ListPagesExample
    {
        public void main()
        {

            var apiInstance = new PagesApi();
            var docId = docId_example;  // string | ID of the doc.
            var limit = 56;  // int? | Maximum number of results to return in this query. (optional) 
            var pageToken = pageToken_example;  // string | An opaque token used to fetch the next page of results. (optional) 

            try
            {
                // List pages
                PageList result = apiInstance.ListPages(docId, limit, pageToken);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PagesApi.ListPages: " + e.Message );
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

### Return type

[**PageList**](PageList.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="updatepage"></a>
# **UpdatePage**
> PageUpdateResult UpdatePage (PageUpdate body, string docId, string pageIdOrName)

Update a page

Update properties for a page.

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class UpdatePageExample
    {
        public void main()
        {

            var apiInstance = new PagesApi();
            var body = new PageUpdate(); // PageUpdate | Parameters for updating a page.
            var docId = docId_example;  // string | ID of the doc.
            var pageIdOrName = pageIdOrName_example;  // string | ID or name of the page. Names are discouraged because they're easily prone to being changed by users. If you're using a name, be sure to URI-encode it. If you provide a name and there are multiple pages with the same name, an arbitrary one will be selected. 

            try
            {
                // Update a page
                PageUpdateResult result = apiInstance.UpdatePage(body, docId, pageIdOrName);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PagesApi.UpdatePage: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **body** | [**PageUpdate**](PageUpdate.md)| Parameters for updating a page. | 
 **docId** | **string**| ID of the doc. | 
 **pageIdOrName** | **string**| ID or name of the page. Names are discouraged because they&#x27;re easily prone to being changed by users. If you&#x27;re using a name, be sure to URI-encode it. If you provide a name and there are multiple pages with the same name, an arbitrary one will be selected.  | 

### Return type

[**PageUpdateResult**](PageUpdateResult.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
