# IO.Swagger.Api.ControlsApi

All URIs are relative to *https://coda.io/apis/v1*

Method | HTTP request | Description
------------- | ------------- | -------------
[**GetControl**](ControlsApi.md#getcontrol) | **GET** /docs/{docId}/controls/{controlIdOrName} | Get a control
[**ListControls**](ControlsApi.md#listcontrols) | **GET** /docs/{docId}/controls | List controls

<a name="getcontrol"></a>
# **GetControl**
> Control GetControl (string docId, string controlIdOrName)

Get a control

Returns info on a control.

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class GetControlExample
    {
        public void main()
        {

            var apiInstance = new ControlsApi();
            var docId = docId_example;  // string | ID of the doc.
            var controlIdOrName = controlIdOrName_example;  // string | ID or name of the control. Names are discouraged because they're easily prone to being changed by users. If you're using a name, be sure to URI-encode it.

            try
            {
                // Get a control
                Control result = apiInstance.GetControl(docId, controlIdOrName);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ControlsApi.GetControl: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **docId** | **string**| ID of the doc. | 
 **controlIdOrName** | **string**| ID or name of the control. Names are discouraged because they&#x27;re easily prone to being changed by users. If you&#x27;re using a name, be sure to URI-encode it. | 

### Return type

[**Control**](Control.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="listcontrols"></a>
# **ListControls**
> ControlList ListControls (string docId, int? limit = null, string pageToken = null, SortBy sortBy = null)

List controls

Returns a list of controls in a Coda doc.

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class ListControlsExample
    {
        public void main()
        {

            var apiInstance = new ControlsApi();
            var docId = docId_example;  // string | ID of the doc.
            var limit = 56;  // int? | Maximum number of results to return in this query. (optional) 
            var pageToken = pageToken_example;  // string | An opaque token used to fetch the next page of results. (optional) 
            var sortBy = new SortBy(); // SortBy | Determines how to sort the given objects. (optional) 

            try
            {
                // List controls
                ControlList result = apiInstance.ListControls(docId, limit, pageToken, sortBy);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ControlsApi.ListControls: " + e.Message );
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

### Return type

[**ControlList**](ControlList.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
