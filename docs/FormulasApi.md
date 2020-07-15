# IO.Swagger.Api.FormulasApi

All URIs are relative to *https://coda.io/apis/v1*

Method | HTTP request | Description
------------- | ------------- | -------------
[**GetFormula**](FormulasApi.md#getformula) | **GET** /docs/{docId}/formulas/{formulaIdOrName} | Get a formula
[**ListFormulas**](FormulasApi.md#listformulas) | **GET** /docs/{docId}/formulas | List formulas

<a name="getformula"></a>
# **GetFormula**
> Formula GetFormula (string docId, string formulaIdOrName)

Get a formula

Returns info on a formula.

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class GetFormulaExample
    {
        public void main()
        {

            var apiInstance = new FormulasApi();
            var docId = docId_example;  // string | ID of the doc.
            var formulaIdOrName = formulaIdOrName_example;  // string | ID or name of the formula. Names are discouraged because they're easily prone to being changed by users. If you're using a name, be sure to URI-encode it.

            try
            {
                // Get a formula
                Formula result = apiInstance.GetFormula(docId, formulaIdOrName);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling FormulasApi.GetFormula: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **docId** | **string**| ID of the doc. | 
 **formulaIdOrName** | **string**| ID or name of the formula. Names are discouraged because they&#x27;re easily prone to being changed by users. If you&#x27;re using a name, be sure to URI-encode it. | 

### Return type

[**Formula**](Formula.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="listformulas"></a>
# **ListFormulas**
> FormulaList ListFormulas (string docId, int? limit = null, string pageToken = null, SortBy sortBy = null)

List formulas

Returns a list of named formulas in a Coda doc.

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class ListFormulasExample
    {
        public void main()
        {

            var apiInstance = new FormulasApi();
            var docId = docId_example;  // string | ID of the doc.
            var limit = 56;  // int? | Maximum number of results to return in this query. (optional) 
            var pageToken = pageToken_example;  // string | An opaque token used to fetch the next page of results. (optional) 
            var sortBy = new SortBy(); // SortBy | Determines how to sort the given objects. (optional) 

            try
            {
                // List formulas
                FormulaList result = apiInstance.ListFormulas(docId, limit, pageToken, sortBy);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling FormulasApi.ListFormulas: " + e.Message );
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

[**FormulaList**](FormulaList.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
