<a name="readme-top"></a>

<br />
<div align="center">
  <h1 align="center">Smart Nutri Food Order</h1>
  <p align="center">
    A smart food ordering platform with an AI-powered nutritionist assistant
    <br />
    <br />
    <a href="https://github.com/anasinik/smart-nutri-food-order/issues/new?labels=bug">Report Bug</a>
    ·
    <a href="https://github.com/anasinik/smart-nutri-food-order/issues/new?labels=enhancement">Request Feature</a>
  </p>
</div>

---

## Table of Contents

1. [About The Project](#about-the-project)
2. [Built With](#built-with)
3. [Architecture](#architecture)
4. [Getting Started](#getting-started)
   - [Prerequisites](#prerequisites)
   - [Installation](#installation)
5. [Usage](#usage)

---

## About The Project

**Smart Nutri Food Order** is a web platform that allows users to browse restaurants and their menus, place food orders, and get personalized meal recommendations from an AI-powered nutritionist assistant.

The platform supports three types of users:
- **Admin** — manages the platform and adds restaurant managers
- **Restaurant Manager** — manages their restaurant's menu and dishes
- **Customer** — browses restaurants, places orders, and interacts with the smart assistant

### Smart Nutritionist Assistant

The highlight of the platform is the **AI nutritionist** — a RAG-based model powered by OpenAI. Customers can describe their nutritional needs (calories, proteins, sugar limits, etc.) and the assistant recommends real dishes available on the platform. The knowledge base is enriched with local restaurant and food data (e.g. Novi Sad area), making recommendations contextually relevant.

---

## Built With

**Backend**

[![.NET](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)

**Frontend**

[![Angular](https://img.shields.io/badge/Angular-DD0031?style=for-the-badge&logo=angular&logoColor=white)](https://angular.io/)

**AI**

[![OpenAI](https://img.shields.io/badge/OpenAI-412991?style=for-the-badge&logo=openai&logoColor=white)](https://openai.com/)

---

## Architecture

### Backend — Clean Architecture

The backend is built using **.NET** following the **Clean Architecture** pattern, organized into clearly separated layers:

```
📦 Solution
 ┣ 📂 Domain
 ┣ 📂 Application
 ┣ 📂 Infrastructure
 ┗ 📂 Web (API)
```

This structure ensures that business logic is fully decoupled from infrastructure concerns, making the codebase testable, maintainable, and easy to extend.

### Frontend — Single Page Application

The frontend is an **Angular SPA** that communicates with the backend via REST API. It provides a seamless, dynamic user experience without full page reloads.

---

## Getting Started

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) (version 8 or later)
- [Node.js & npm](https://nodejs.org/)
- [Angular CLI](https://angular.io/cli)
- OpenAI API key

### Installation

1. **Clone the repository**
   ```sh
   git clone https://github.com/anasinik/smart-nutri-food-order.git
   ```

2. **Backend setup**
   ```sh
   cd backend/FoodOrderApi/src/FoodOrderApi.Web
   ```
   Configure your connection string and OpenAI key in `appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "your_connection_string"
     },
     "OpenAI": {
       "ApiKey": "your_openai_api_key"
     }
   }
   ```
   Run the API:
   ```sh
   dotnet run
   ```

3. **Frontend setup**
   ```sh
   cd frontend
   npm install
   ng serve
   ```

4. Open your browser at `http://localhost:4200`

---

## Usage

- Register or log in as a customer to browse restaurants and place orders
- Use the **Smart Nutritionist** chat to find meals that fit your dietary goals — just describe what you're looking for (e.g. *"high protein, under 600 calories"*) and the assistant will suggest available dishes from real restaurants
- Admin users can manage restaurant managers through the admin panel
- Restaurant managers can add and update their menu items

<p align="right">(<a href="#readme-top">back to top</a>)</p>
