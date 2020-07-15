# IO.Swagger.Model.Row
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Id** | **string** | ID of the row. | 
**Type** | **string** | The type of this resource. | 
**Href** | **string** | API link to the row. | 
**Name** | **string** | The display name of the row, based on its identifying column. | 
**Index** | **int?** | Index of the row within the table. | 
**BrowserLink** | **string** | Browser-friendly link to the row. | 
**CreatedAt** | **DateTime?** | Timestamp for when the row was created. | 
**UpdatedAt** | **DateTime?** | Timestamp for when the row was last modified. | 
**Values** | [**Dictionary&lt;string, CellValue&gt;**](CellValue.md) | Values for a specific row, represented as a hash of column IDs (or names with &#x60;useColumnNames&#x60;) to values.  | 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

