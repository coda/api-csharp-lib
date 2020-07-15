# IO.Swagger.Api.MiscellaneousApi

All URIs are relative to *https://coda.io/apis/v1*

Method | HTTP request | Description
------------- | ------------- | -------------
[**GetMutationStatus**](MiscellaneousApi.md#getmutationstatus) | **GET** /mutationStatus/{requestId} | Get mutation status
[**ResolveBrowserLink**](MiscellaneousApi.md#resolvebrowserlink) | **GET** /resolveBrowserLink | Resolve browser link

<a name="getmutationstatus"></a>
# **GetMutationStatus**
> MutationStatus GetMutationStatus (string requestId)

Get mutation status

Get the status for an asynchronous mutation to know whether or not it has been completed. Each API endpoint that mutates a document will return a request id that you can pass to this endpoint to check the completion status. Status information is not guaranteed to be available for more than one day after the mutation was completed. It is intended to be used shortly after the request was made. 

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class GetMutationStatusExample
    {
        public void main()
        {

            var apiInstance = new MiscellaneousApi();
            var requestId = requestId_example;  // string | ID of the request.

            try
            {
                // Get mutation status
                MutationStatus result = apiInstance.GetMutationStatus(requestId);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling MiscellaneousApi.GetMutationStatus: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **requestId** | **string**| ID of the request. | 

### Return type

[**MutationStatus**](MutationStatus.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="resolvebrowserlink"></a>
# **ResolveBrowserLink**
> ApiLink ResolveBrowserLink (string url, bool? degradeGracefully = null)

Resolve browser link

Given a browser link to a Coda object, attempts to find it and return metadata that can be used to get more info on it. Returns a 400 if the URL does not appear to be a Coda URL or a 404 if the resource cannot be located with the current credentials. 

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class ResolveBrowserLinkExample
    {
        public void main()
        {

            var apiInstance = new MiscellaneousApi();
            var url = url_example;  // string | The browser link to try to resolve.
            var degradeGracefully = true;  // bool? | By default, attempting to resolve the Coda URL of a deleted object will result in an error. If this flag is set, the next-available object, all the way up to the doc itself, will be resolved.  (optional) 

            try
            {
                // Resolve browser link
                ApiLink result = apiInstance.ResolveBrowserLink(url, degradeGracefully);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling MiscellaneousApi.ResolveBrowserLink: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **url** | **string**| The browser link to try to resolve. | 
 **degradeGracefully** | **bool?**| By default, attempting to resolve the Coda URL of a deleted object will result in an error. If this flag is set, the next-available object, all the way up to the doc itself, will be resolved.  | [optional] 

### Return type

[**ApiLink**](ApiLink.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
