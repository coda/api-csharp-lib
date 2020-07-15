# IO.Swagger.Api.PublishingApi

All URIs are relative to *https://coda.io/apis/v1*

Method | HTTP request | Description
------------- | ------------- | -------------
[**ListCategories**](PublishingApi.md#listcategories) | **GET** /categories | Get doc categories
[**PublishDoc**](PublishingApi.md#publishdoc) | **PUT** /docs/{docId}/publish | Publish doc
[**UnpublishDoc**](PublishingApi.md#unpublishdoc) | **DELETE** /docs/{docId}/publish | Unpublish doc

<a name="listcategories"></a>
# **ListCategories**
> DocCategoryList ListCategories ()

Get doc categories

Gets all available doc categories.

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class ListCategoriesExample
    {
        public void main()
        {

            var apiInstance = new PublishingApi();

            try
            {
                // Get doc categories
                DocCategoryList result = apiInstance.ListCategories();
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PublishingApi.ListCategories: " + e.Message );
            }
        }
    }
}
```

### Parameters
This endpoint does not need any parameter.

### Return type

[**DocCategoryList**](DocCategoryList.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="publishdoc"></a>
# **PublishDoc**
> PublishResult PublishDoc (DocPublish body, string docId)

Publish doc

Update publish settings for a doc.

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class PublishDocExample
    {
        public void main()
        {

            var apiInstance = new PublishingApi();
            var body = new DocPublish(); // DocPublish | Parameters for changing publish settings.
            var docId = docId_example;  // string | ID of the doc.

            try
            {
                // Publish doc
                PublishResult result = apiInstance.PublishDoc(body, docId);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PublishingApi.PublishDoc: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **body** | [**DocPublish**](DocPublish.md)| Parameters for changing publish settings. | 
 **docId** | **string**| ID of the doc. | 

### Return type

[**PublishResult**](PublishResult.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="unpublishdoc"></a>
# **UnpublishDoc**
> UnpublishResult UnpublishDoc (string docId)

Unpublish doc

Unpublishes a doc.

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class UnpublishDocExample
    {
        public void main()
        {

            var apiInstance = new PublishingApi();
            var docId = docId_example;  // string | ID of the doc.

            try
            {
                // Unpublish doc
                UnpublishResult result = apiInstance.UnpublishDoc(docId);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PublishingApi.UnpublishDoc: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **docId** | **string**| ID of the doc. | 

### Return type

[**UnpublishResult**](UnpublishResult.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
