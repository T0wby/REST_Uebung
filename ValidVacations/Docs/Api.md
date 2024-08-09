# Valid Vacations API

- [Valid Vacations API](#buber-vacation-api)
  - [Create Vacation](#create-vacation)
    - [Create Vacation Request](#create-vacation-request)
    - [Create Vacation Response](#create-vacation-response)
  - [Get Vacation](#get-vacation)
    - [Get Vacation Request](#get-vacation-request)
    - [Get Vacation Response](#get-vacation-response)
  - [Update Vacation](#update-vacation)
    - [Update Vacation Request](#update-vacation-request)
    - [Update Vacation Response](#update-vacation-response)
  - [Delete Vacation](#delete-vacation)
    - [Delete Vacation Request](#delete-vacation-request)
    - [Delete Vacation Response](#delete-vacation-response)

## Create Vacation

### Create Vacation Request

```js
POST /vacations
```

```json
{
    "title": "Japan Railroad-Adventure: City Hopping im Shinkansen",
    "description": "Following Train Tracks: Von Tokyo nach Hiroshima!",
    "startDate": "2025-04-08",
    "endDate": "2025-04-22",
    "length": "14",
    "price": "189900"
}
```

### Create Vacation Response

```js
201 Created
```

```yml
Location: {{host}}/vacations/{{id}}
```

```json
{
    "id": "00000000-0000-0000-0000-000000000000",
    "title": "Japan Railroad-Adventure: City Hopping im Shinkansen",
    "description": "Following Train Tracks: Von Tokyo nach Hiroshima!",
    "startDate": "2025-04-08",
    "endDate": "2025-04-22",
    "lastModifiedDateTime": "2022-04-06T12:00:00",
    "length": "14",
    "price": "189900"
}
```

## Get Vacation

### Get Vacation Request

```js
GET /vacations/{{id}}
```

### Get Vacation Response

```js
200 Ok
```

```json
{
    "id": "00000000-0000-0000-0000-000000000000",
    "title": "Japan Railroad-Adventure: City Hopping im Shinkansen",
    "description": "Following Train Tracks: Von Tokyo nach Hiroshima!",
    "startDate": "2025-04-08",
    "endDate": "2025-04-22",
    "lastModifiedDateTime": "2022-04-06T12:00:00",
    "length": "14",
    "price": "189900"
}
```

## Update Vacation

### Update Vacation Request

```js
PUT /vacations/{{id}}
```

```json
{
    "title": "Japan Railroad-Adventure: City Hopping im Shinkansen",
    "description": "Following Train Tracks: Von Tokyo nach Hiroshima!",
    "startDate": "2025-04-08",
    "endDate": "2025-04-22",
    "length": "14",
    "price": "189900"
}
```

### Update Vacation Response

```js
204 No Content
```

or

```js
201 Created
```

```yml
Location: {{host}}/Vacations/{{id}}
```

## Delete Vacation

### Delete Vacation Request

```js
DELETE /vacations/{{id}}
```

### Delete Vacation Response

```js
204 No Content
```