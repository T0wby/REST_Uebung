# Towby Jobs API

- [Towby Jobs API](#towby-jobs-api)
  - [Create Job](#create-job)
    - [Create Job Request](#create-job-request)
    - [Create Job Response](#create-job-response)
  - [Get Job](#get-job)
    - [Get Job Request](#get-job-request)
    - [Get Job Response](#get-job-response)
  - [Update Job](#update-job)
    - [Update Job Request](#update-job-request)
    - [Update Job Response](#update-job-response)
  - [Delete Job](#delete-job)
    - [Delete Job Request](#delete-job-request)
    - [Delete Job Response](#delete-job-response)

## Create Job

### Create Job Request

```js
POST /jobs
```

```json
{
    "companyName": "Arvato SE",
    "position": "(Junior) Softwareentwickler C# (m/w/x)",
    "link": "https://jobsearch.createyourowncareer.com/ARVATO/job/G%C3%BCtersloh-%28Junior%29-Softwareentwickler-C-%28mwx%29-NW-33333/1069456701/",
    "contact": "Michelle Hambrink",
    "applicationStatus": "InProgress",
    "applicationDate": "2022-04-08"
}
```

### Create Job Response

```js
201 Created
```

```yml
Location: {{host}}/Jobs/{{id}}
```

```json
{
    "id": "00000000-0000-0000-0000-000000000000",
    "companyName": "Arvato SE",
    "position": "(Junior) Softwareentwickler C# (m/w/x)",
    "link": "https://jobsearch.createyourowncareer.com/ARVATO/job/G%C3%BCtersloh-%28Junior%29-Softwareentwickler-C-%28mwx%29-NW-33333/1069456701/",
    "contact": "Michelle Hambrink",
    "applicationStatus": "InProgress",
    "applicationDate": "2022-04-08",
    "lastModifiedDateTime": "2022-04-06T12:00:00"
}
```

## Get Job

### Get Job Request

```js
GET /jobs/{{id}}
```

### Get Job Response

```js
200 Ok
```

```json
{
    "id": "00000000-0000-0000-0000-000000000000",
    "companyName": "Arvato SE",
    "position": "(Junior) Softwareentwickler C# (m/w/x)",
    "link": "https://jobsearch.createyourowncareer.com/ARVATO/job/G%C3%BCtersloh-%28Junior%29-Softwareentwickler-C-%28mwx%29-NW-33333/1069456701/",
    "contact": "Michelle Hambrink",
    "applicationStatus": "InProgress",
    "applicationDate": "2022-04-08",
    "lastModifiedDateTime": "2022-04-06T12:00:00"
}
```

## Update Job

### Update Job Request

```js
PUT /jobs/{{id}}
```

```json
{
    "companyName": "Arvato SE",
    "position": "(Junior) Softwareentwickler C# (m/w/x)",
    "link": "https://jobsearch.createyourowncareer.com/ARVATO/job/G%C3%BCtersloh-%28Junior%29-Softwareentwickler-C-%28mwx%29-NW-33333/1069456701/",
    "contact": "Michelle Hambrink",
    "applicationStatus": "InProgress",
    "applicationDate": "2022-04-08"
}
```

### Update Job Response

```js
204 No Content
```

or

```js
201 Created
```

```yml
Location: {{host}}/Jobs/{{id}}
```

## Delete Job

### Delete Job Request

```js
DELETE /jobs/{{id}}
```

### Delete Job Response

```js
204 No Content
```