# IO.Swagger.Api.AccountApi

All URIs are relative to *https://coda.io/apis/v1*

Method | HTTP request | Description
------------- | ------------- | -------------
[**Whoami**](AccountApi.md#whoami) | **GET** /whoami | Get user info

<a name="whoami"></a>
# **Whoami**
> User Whoami ()

Get user info

Returns basic info about the current user.

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class WhoamiExample
    {
        public void main()
        {

            var apiInstance = new AccountApi();

            try
            {
                // Get user info
                User result = apiInstance.Whoami();
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling AccountApi.Whoami: " + e.Message );
            }
        }
    }
}
```

### Parameters
This endpoint does not need any parameter.

### Return type

[**User**](User.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
