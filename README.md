# json2xml
Vanilla transformation of JSON text into XML text without the use of JSON nor XML libraries. For example:

```json
{ 
    "image1": ".\\media\\logo_dark.png",
    "PurchaseOrderNumber": 99503,
    "OrderDate": "1999-10-20",
    "Advise": false,
    "Shipping": { "Name": "Ellen Adams", "Street": "123 Maple Street" },
    "Items": [
        { "Item": { "PartNumber": "872-AA", "ProductName": "Lawnmower", "Quantity": 1, "USPrice": 148.95, "Comment": "Confirm this is electric" } },
        { "Item": { "PartNumber": "926-AA", "ProductName": "Baby Monitor", "Quantity": 2, "USPrice": 39.98, "ShipDate": "1999-05-21" } },
        { "Item": { "PartNumber": "934-AA", "ProductName": "Troll", "Quantity": 20, "USPrice": 39.98, "ShipDate": "1999-05-21" } }
    ]
}
```
into

```xml
<root>
    <image1>.\\media\\logo_dark.png</image1>
    <PurchaseOrderNumber>99503</PurchaseOrderNumber>
    <OrderDate>1999-10-20</OrderDate>
    <Advise>false</Advise>
    <Shipping>
        <Name>Ellen Adams</Name>
        <Street>123 Maple Street</Street>
    </Shipping>
    <Items>
        <Item>
            <PartNumber>872-AA</PartNumber>
            <ProductName>Lawnmower</ProductName>
            <Quantity>1</Quantity>
            <USPrice>148.95</USPrice>
            <Comment>Confirm this is electric</Comment>
        </Item>
        <Item>
            <PartNumber>926-AA</PartNumber>
            <ProductName>Baby Monitor</ProductName>
            <Quantity>2</Quantity>
            <USPrice>39.98</USPrice>
            <ShipDate>1999-05-21</ShipDate>
        </Item>
        <Item>
            <PartNumber>934-AA</PartNumber>
            <ProductName>Troll</ProductName>
            <Quantity>20</Quantity>
            <USPrice>39.98</USPrice>
            <ShipDate>1999-05-21</ShipDate>
        </Item>
    </Items>
</root>
```

```cs
Console.Write(json2xml(@"{""greet"":""Hello world""}");

Output:
<root><greet>Hello world</greet></root>
```
