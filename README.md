# store-service

Store item sale price calculator.

## Exposed below APIs

* GET: http://localhost:59443/api/Discounts/Items - Gets all the master discount info for items.
* GET: http://localhost:59443/api/Discounts/WeekDays - Gets all the master discount info for weekdays.
* POST: http://localhost:59443/api/Checkout - Calculates the discount.
  Sample inputs body
      
      {
        "billDate": "2022-05-30T03:27:05.926Z",
        "items": [
          {
            "itemId": 4,
            "quantity": 6,
            "unitPrice": 10
          }
        ]
      }
      
      
      {
        "billDate": "2022-05-30T03:27:05.926Z",
        "items": [
          {
            "itemId": 1,
            "quantity": 2,
            "unitPrice": 10
          },
        {
            "itemId": 2,
            "quantity": 2,
            "unitPrice": 20
          },
        {
            "itemId": 4,
            "quantity": 3,
            "unitPrice": 30
          },
        {
            "itemId": 6,
            "quantity": 1,
            "unitPrice": 50
          }
        ]
      }

## Other details

* All the master values are hardcoded in appsettings.json file.
* Added Swagger to execute the APIs. 
