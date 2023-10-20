# json2xml
Plain text transformation of JSON text into XML text without the use of JSON nor XML libraries. As an example:

```json
{ 
    "image1": ".\\media\\logo_dark.png",
    "PurchaseOrderNumber": 99503,
    "OrderDate": "1999-10-20",
    "Shipping": {
        "Name": "Ellen Adams",
        "Street": "123 Maple Street"
    }
}
```
into

```xml
<root>
  <image1>.\\media\\logo_dark.png</image1>
  <PurchaseOrderNumber>99503</PurchaseOrderNumber>
  <OrderDate>1999-10-20</OrderDate>
  <Shipping>
    <Name>Ellen Adams</Name>
    <Street>123 Maple Street</Street>
  </Shipping>
</root>
```

```cs
Console.Write(json2xml(@"{""greet"":""Hello world""}");

Output:
<root><greet>Hello world</greet></root>
```
