# IO.Swagger.Api.AclApi

All URIs are relative to *https://coda.io/apis/v1*

Method | HTTP request | Description
------------- | ------------- | -------------
[**DeletePermission**](AclApi.md#deletepermission) | **DELETE** /docs/{docId}/acl/permissions/{permissionId} | Delete permission
[**GetAclMetadata**](AclApi.md#getaclmetadata) | **GET** /docs/{docId}/acl/metadata | AclMetadata
[**GetAclPermissions**](AclApi.md#getaclpermissions) | **GET** /docs/{docId}/acl/permissions | Acl
[**ShareDoc**](AclApi.md#sharedoc) | **POST** /docs/{docId}/acl/permissions | Add permission

<a name="deletepermission"></a>
# **DeletePermission**
> DeletePermissionResult DeletePermission (string docId, string permissionId)

Delete permission

Deletes an existing permission. 

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class DeletePermissionExample
    {
        public void main()
        {

            var apiInstance = new AclApi();
            var docId = docId_example;  // string | ID of the doc.
            var permissionId = permissionId_example;  // string | ID of a permission on a doc.

            try
            {
                // Delete permission
                DeletePermissionResult result = apiInstance.DeletePermission(docId, permissionId);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling AclApi.DeletePermission: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **docId** | **string**| ID of the doc. | 
 **permissionId** | **string**| ID of a permission on a doc. | 

### Return type

[**DeletePermissionResult**](DeletePermissionResult.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="getaclmetadata"></a>
# **GetAclMetadata**
> AclMetadata GetAclMetadata (string docId)

AclMetadata

Returns metadata associated with ACL for this Coda doc.

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class GetAclMetadataExample
    {
        public void main()
        {

            var apiInstance = new AclApi();
            var docId = docId_example;  // string | ID of the doc.

            try
            {
                // AclMetadata
                AclMetadata result = apiInstance.GetAclMetadata(docId);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling AclApi.GetAclMetadata: " + e.Message );
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

[**AclMetadata**](AclMetadata.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="getaclpermissions"></a>
# **GetAclPermissions**
> Acl GetAclPermissions (string docId, int? limit = null, string pageToken = null)

Acl

Returns a list of permissionos for this Coda doc.

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class GetAclPermissionsExample
    {
        public void main()
        {

            var apiInstance = new AclApi();
            var docId = docId_example;  // string | ID of the doc.
            var limit = 56;  // int? | Maximum number of results to return in this query. (optional) 
            var pageToken = pageToken_example;  // string | An opaque token used to fetch the next page of results. (optional) 

            try
            {
                // Acl
                Acl result = apiInstance.GetAclPermissions(docId, limit, pageToken);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling AclApi.GetAclPermissions: " + e.Message );
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

[**Acl**](Acl.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="sharedoc"></a>
# **ShareDoc**
> AddPermissionResult ShareDoc (AddPermission body, string docId)

Add permission

Adds a new permission to the doc. 

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class ShareDocExample
    {
        public void main()
        {

            var apiInstance = new AclApi();
            var body = new AddPermission(); // AddPermission | Parameters for adding the new permission.
            var docId = docId_example;  // string | ID of the doc.

            try
            {
                // Add permission
                AddPermissionResult result = apiInstance.ShareDoc(body, docId);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling AclApi.ShareDoc: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **body** | [**AddPermission**](AddPermission.md)| Parameters for adding the new permission. | 
 **docId** | **string**| ID of the doc. | 

### Return type

[**AddPermissionResult**](AddPermissionResult.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
