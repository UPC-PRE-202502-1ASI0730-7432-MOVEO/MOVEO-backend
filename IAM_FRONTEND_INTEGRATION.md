# 🔐 Guía de Integración IAM con JWT - Frontend

## Resumen

El backend ahora soporta autenticación JWT completa. Este documento describe cómo integrar el frontend Vue.js con el sistema de autenticación.

---

## 📍 Endpoints de Autenticación

Base URL: `http://localhost:8080/api/v1/auth`

| Método | Endpoint | Descripción | Auth Requerida |
|--------|----------|-------------|----------------|
| POST | `/register` | Registrar nuevo usuario | ❌ No |
| POST | `/login` | Iniciar sesión | ❌ No |
| POST | `/refresh-token` | Renovar token expirado | ❌ No |
| POST | `/logout` | Cerrar sesión | ✅ Sí |
| GET | `/me` | Obtener usuario actual | ✅ Sí |
| POST | `/change-password` | Cambiar contraseña | ✅ Sí |

---

## 📝 Especificación de Endpoints

### 1. POST `/api/v1/auth/register`

Registra un nuevo usuario en el sistema.

**Request Body:**
```json
{
  "firstName": "Juan",
  "lastName": "García",
  "email": "juan@example.com",
  "password": "MiPassword123!",
  "phone": "+51987654321",
  "dni": "12345678",
  "licenseNumber": "Q12345678",
  "address": "Av. Example 123, Lima",
  "role": "renter",
  "preferences": {
    "language": "es",
    "emailNotifications": true,
    "pushNotifications": true,
    "smsNotifications": false,
    "autoAcceptRentals": false,
    "minimumRentalDays": 1,
    "instantBooking": false
  }
}
```

**Campos Requeridos:** `firstName`, `lastName`, `email`, `password`

**Campos Opcionales:** `phone`, `dni`, `licenseNumber`, `address`, `role` (default: "renter"), `preferences`

**Response (201 Created):**
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "refreshToken": "dGhpcyBpcyBhIHJlZnJlc2ggdG9rZW4...",
  "expiresAt": "2025-12-02T14:30:00Z",
  "user": {
    "id": 1,
    "firstName": "Juan",
    "lastName": "García",
    "email": "juan@example.com",
    "phone": "+51987654321",
    "dni": "12345678",
    "licenseNumber": "Q12345678",
    "role": "renter",
    "address": "Av. Example 123, Lima"
  }
}
```

**Errores:**
- `400 Bad Request` - Campos requeridos faltantes
- `409 Conflict` - Email ya registrado

---

### 2. POST `/api/v1/auth/login`

Autentica un usuario existente.

**Request Body:**
```json
{
  "email": "juan@example.com",
  "password": "MiPassword123!"
}
```

**Response (200 OK):**
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "refreshToken": "dGhpcyBpcyBhIHJlZnJlc2ggdG9rZW4...",
  "expiresAt": "2025-12-02T14:30:00Z",
  "user": {
    "id": 1,
    "firstName": "Juan",
    "lastName": "García",
    "email": "juan@example.com",
    "phone": "+51987654321",
    "dni": "12345678",
    "licenseNumber": "Q12345678",
    "role": "renter",
    "address": "Av. Example 123, Lima"
  }
}
```

**Errores:**
- `400 Bad Request` - Campos requeridos faltantes
- `401 Unauthorized` - Credenciales inválidas

---

### 3. POST `/api/v1/auth/refresh-token`

Renueva un token de acceso expirado usando el refresh token.

**Request Body:**
```json
{
  "accessToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "refreshToken": "dGhpcyBpcyBhIHJlZnJlc2ggdG9rZW4..."
}
```

**Response (200 OK):**
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...(nuevo)",
  "refreshToken": "bmV3IHJlZnJlc2ggdG9rZW4...(nuevo)",
  "expiresAt": "2025-12-02T15:30:00Z",
  "user": {
    "id": 1,
    "firstName": "Juan",
    "lastName": "García",
    "email": "juan@example.com",
    "phone": "+51987654321",
    "dni": "12345678",
    "licenseNumber": "Q12345678",
    "role": "renter",
    "address": "Av. Example 123, Lima"
  }
}
```

**Errores:**
- `400 Bad Request` - Tokens faltantes
- `401 Unauthorized` - Refresh token inválido o expirado

---

### 4. POST `/api/v1/auth/logout`

Cierra la sesión e invalida el refresh token.

**Headers:**
```
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```

**Response (204 No Content):** Sin cuerpo

**Errores:**
- `401 Unauthorized` - Token inválido o no proporcionado

---

### 5. GET `/api/v1/auth/me`

Obtiene la información del usuario autenticado.

**Headers:**
```
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```

**Response (200 OK):**
```json
{
  "id": 1,
  "firstName": "Juan",
  "lastName": "García",
  "email": "juan@example.com",
  "phone": "+51987654321",
  "dni": "12345678",
  "licenseNumber": "Q12345678",
  "role": "renter",
  "address": "Av. Example 123, Lima"
}
```

**Errores:**
- `401 Unauthorized` - Token inválido o no proporcionado
- `404 Not Found` - Usuario no encontrado

---

### 6. POST `/api/v1/auth/change-password`

Cambia la contraseña del usuario autenticado.

**Headers:**
```
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```

**Request Body:**
```json
{
  "currentPassword": "MiPasswordActual123!",
  "newPassword": "MiNuevaPassword456!"
}
```

**Response (200 OK):**
```json
{
  "message": "Password changed successfully"
}
```

**Errores:**
- `400 Bad Request` - Contraseña actual incorrecta o campos faltantes
- `401 Unauthorized` - Token inválido

---

## 🛠️ Implementación en Vue.js

### 1. Servicio de Autenticación (`src/services/authService.js`)

```javascript
import axios from 'axios';

const API_URL = 'http://localhost:8080/api/v1/auth';

class AuthService {
  async login(email, password) {
    const response = await axios.post(`${API_URL}/login`, {
      email,
      password
    });
    
    if (response.data.token) {
      this.setTokens(response.data);
    }
    
    return response.data;
  }

  async register(userData) {
    const response = await axios.post(`${API_URL}/register`, userData);
    
    if (response.data.token) {
      this.setTokens(response.data);
    }
    
    return response.data;
  }

  async refreshToken() {
    const accessToken = localStorage.getItem('accessToken');
    const refreshToken = localStorage.getItem('refreshToken');
    
    if (!accessToken || !refreshToken) {
      throw new Error('No tokens available');
    }

    const response = await axios.post(`${API_URL}/refresh-token`, {
      accessToken,
      refreshToken
    });
    
    if (response.data.token) {
      this.setTokens(response.data);
    }
    
    return response.data;
  }

  async logout() {
    try {
      await axios.post(`${API_URL}/logout`, {}, {
        headers: this.getAuthHeader()
      });
    } finally {
      this.clearTokens();
    }
  }

  async getCurrentUser() {
    const response = await axios.get(`${API_URL}/me`, {
      headers: this.getAuthHeader()
    });
    return response.data;
  }

  async changePassword(currentPassword, newPassword) {
    const response = await axios.post(`${API_URL}/change-password`, {
      currentPassword,
      newPassword
    }, {
      headers: this.getAuthHeader()
    });
    return response.data;
  }

  // Helper methods
  setTokens(authResponse) {
    localStorage.setItem('accessToken', authResponse.token);
    localStorage.setItem('refreshToken', authResponse.refreshToken);
    localStorage.setItem('tokenExpiresAt', authResponse.expiresAt);
    localStorage.setItem('user', JSON.stringify(authResponse.user));
  }

  clearTokens() {
    localStorage.removeItem('accessToken');
    localStorage.removeItem('refreshToken');
    localStorage.removeItem('tokenExpiresAt');
    localStorage.removeItem('user');
  }

  getAuthHeader() {
    const token = localStorage.getItem('accessToken');
    return token ? { Authorization: `Bearer ${token}` } : {};
  }

  isAuthenticated() {
    const token = localStorage.getItem('accessToken');
    const expiresAt = localStorage.getItem('tokenExpiresAt');
    
    if (!token || !expiresAt) return false;
    
    return new Date(expiresAt) > new Date();
  }

  getStoredUser() {
    const user = localStorage.getItem('user');
    return user ? JSON.parse(user) : null;
  }
}

export default new AuthService();
```

---

### 2. Axios Interceptor (`src/plugins/axios.js`)

```javascript
import axios from 'axios';
import authService from '@/services/authService';
import router from '@/router';

const api = axios.create({
  baseURL: 'http://localhost:8080/api/v1',
  headers: {
    'Content-Type': 'application/json'
  }
});

// Request interceptor - añade token a cada petición
api.interceptors.request.use(
  (config) => {
    const token = localStorage.getItem('accessToken');
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

// Response interceptor - maneja errores 401 y refresh token
api.interceptors.response.use(
  (response) => response,
  async (error) => {
    const originalRequest = error.config;

    // Si es 401 y no hemos intentado refresh aún
    if (error.response?.status === 401 && !originalRequest._retry) {
      originalRequest._retry = true;

      try {
        // Intentar refresh token
        await authService.refreshToken();
        
        // Reintentar la petición original con el nuevo token
        const token = localStorage.getItem('accessToken');
        originalRequest.headers.Authorization = `Bearer ${token}`;
        return api(originalRequest);
      } catch (refreshError) {
        // Si falla el refresh, logout y redirigir a login
        authService.clearTokens();
        router.push('/login');
        return Promise.reject(refreshError);
      }
    }

    return Promise.reject(error);
  }
);

export default api;
```

---

### 3. Store de Autenticación (Pinia) (`src/stores/auth.js`)

```javascript
import { defineStore } from 'pinia';
import authService from '@/services/authService';

export const useAuthStore = defineStore('auth', {
  state: () => ({
    user: authService.getStoredUser(),
    isAuthenticated: authService.isAuthenticated(),
    loading: false,
    error: null
  }),

  getters: {
    currentUser: (state) => state.user,
    isLoggedIn: (state) => state.isAuthenticated,
    userRole: (state) => state.user?.role || null,
    fullName: (state) => state.user ? `${state.user.firstName} ${state.user.lastName}` : ''
  },

  actions: {
    async login(email, password) {
      this.loading = true;
      this.error = null;
      
      try {
        const response = await authService.login(email, password);
        this.user = response.user;
        this.isAuthenticated = true;
        return response;
      } catch (error) {
        this.error = error.response?.data?.message || 'Error al iniciar sesión';
        throw error;
      } finally {
        this.loading = false;
      }
    },

    async register(userData) {
      this.loading = true;
      this.error = null;
      
      try {
        const response = await authService.register(userData);
        this.user = response.user;
        this.isAuthenticated = true;
        return response;
      } catch (error) {
        this.error = error.response?.data?.message || 'Error al registrar usuario';
        throw error;
      } finally {
        this.loading = false;
      }
    },

    async logout() {
      try {
        await authService.logout();
      } finally {
        this.user = null;
        this.isAuthenticated = false;
      }
    },

    async fetchCurrentUser() {
      try {
        const user = await authService.getCurrentUser();
        this.user = user;
        localStorage.setItem('user', JSON.stringify(user));
        return user;
      } catch (error) {
        this.logout();
        throw error;
      }
    },

    async changePassword(currentPassword, newPassword) {
      this.loading = true;
      try {
        await authService.changePassword(currentPassword, newPassword);
      } finally {
        this.loading = false;
      }
    },

    checkAuth() {
      this.isAuthenticated = authService.isAuthenticated();
      if (!this.isAuthenticated) {
        this.user = null;
      }
    }
  }
});
```

---

### 4. Guard de Rutas (`src/router/guards.js`)

```javascript
import { useAuthStore } from '@/stores/auth';

export function authGuard(to, from, next) {
  const authStore = useAuthStore();
  authStore.checkAuth();

  if (to.meta.requiresAuth && !authStore.isAuthenticated) {
    // Guardar la ruta a la que intentaba acceder
    next({ 
      path: '/login', 
      query: { redirect: to.fullPath } 
    });
  } else if (to.meta.guest && authStore.isAuthenticated) {
    // Si ya está autenticado, redirigir al dashboard
    next('/dashboard');
  } else {
    next();
  }
}

export function roleGuard(allowedRoles) {
  return (to, from, next) => {
    const authStore = useAuthStore();
    
    if (!allowedRoles.includes(authStore.userRole)) {
      next('/unauthorized');
    } else {
      next();
    }
  };
}
```

---

### 5. Configuración del Router (`src/router/index.js`)

```javascript
import { createRouter, createWebHistory } from 'vue-router';
import { authGuard, roleGuard } from './guards';

const routes = [
  {
    path: '/login',
    name: 'Login',
    component: () => import('@/views/auth/LoginView.vue'),
    meta: { guest: true }
  },
  {
    path: '/register',
    name: 'Register',
    component: () => import('@/views/auth/RegisterView.vue'),
    meta: { guest: true }
  },
  {
    path: '/dashboard',
    name: 'Dashboard',
    component: () => import('@/views/DashboardView.vue'),
    meta: { requiresAuth: true }
  },
  {
    path: '/profile',
    name: 'Profile',
    component: () => import('@/views/ProfileView.vue'),
    meta: { requiresAuth: true }
  },
  {
    path: '/admin',
    name: 'Admin',
    component: () => import('@/views/AdminView.vue'),
    meta: { requiresAuth: true },
    beforeEnter: roleGuard(['admin'])
  }
];

const router = createRouter({
  history: createWebHistory(),
  routes
});

router.beforeEach(authGuard);

export default router;
```

---

## 🔄 Flujo de Autenticación

```
┌─────────────────────────────────────────────────────────────────┐
│                    FLUJO DE AUTENTICACIÓN                        │
└─────────────────────────────────────────────────────────────────┘

1. REGISTRO/LOGIN
   ┌─────────┐      POST /register o /login      ┌─────────┐
   │ Frontend│ ─────────────────────────────────▶│ Backend │
   │         │◀───────────────────────────────── │         │
   └─────────┘    { token, refreshToken, user }  └─────────┘
        │
        ▼
   Guardar en localStorage:
   - accessToken
   - refreshToken
   - tokenExpiresAt
   - user

2. PETICIONES AUTENTICADAS
   ┌─────────┐   GET /api/v1/vehicles            ┌─────────┐
   │ Frontend│   Authorization: Bearer <token>   │ Backend │
   │         │ ─────────────────────────────────▶│         │
   │         │◀───────────────────────────────── │         │
   └─────────┘         200 OK + data             └─────────┘

3. TOKEN EXPIRADO (Automático con Interceptor)
   ┌─────────┐   GET /api/v1/vehicles            ┌─────────┐
   │ Frontend│ ─────────────────────────────────▶│ Backend │
   │         │◀───────────────────────────────── │         │
   └─────────┘         401 Unauthorized          └─────────┘
        │
        ▼
   ┌─────────┐   POST /refresh-token             ┌─────────┐
   │ Frontend│ ─────────────────────────────────▶│ Backend │
   │         │◀───────────────────────────────── │         │
   └─────────┘    { newToken, newRefreshToken }  └─────────┘
        │
        ▼
   Reintentar petición original con nuevo token

4. LOGOUT
   ┌─────────┐   POST /logout                    ┌─────────┐
   │ Frontend│ ─────────────────────────────────▶│ Backend │
   │         │◀───────────────────────────────── │         │
   └─────────┘         204 No Content            └─────────┘
        │
        ▼
   Limpiar localStorage y redirigir a /login
```

---

## ⚙️ Configuración JWT

| Parámetro | Valor | Descripción |
|-----------|-------|-------------|
| Token Expiration | 60 minutos | Tiempo de vida del access token |
| Refresh Token Expiration | 7 días | Tiempo de vida del refresh token |
| Algorithm | HS256 | Algoritmo de firma |
| Issuer | MoveoBackend | Emisor del token |
| Audience | MoveoFrontend | Audiencia del token |

---

## 🔒 Roles Disponibles

| Rol | Descripción |
|-----|-------------|
| `renter` | Usuario que alquila vehículos (default) |
| `owner` | Propietario de vehículos |
| `admin` | Administrador del sistema |

---

## ✅ Checklist de Implementación Frontend

- [ ] Instalar dependencias: `axios`, `pinia`
- [ ] Crear servicio de autenticación (`authService.js`)
- [ ] Configurar interceptores de Axios
- [ ] Crear store de Pinia para auth
- [ ] Implementar guards de rutas
- [ ] Crear vista de Login
- [ ] Crear vista de Register
- [ ] Implementar lógica de refresh token automático
- [ ] Manejar logout en toda la app
- [ ] Añadir header Authorization a todas las peticiones

---

## 🧪 Pruebas con cURL

```bash
# Registrar usuario
curl -X POST http://localhost:8080/api/v1/auth/register \
  -H "Content-Type: application/json" \
  -d '{"firstName":"Juan","lastName":"Garcia","email":"juan@test.com","password":"Test123!","role":"renter"}'

# Login
curl -X POST http://localhost:8080/api/v1/auth/login \
  -H "Content-Type: application/json" \
  -d '{"email":"juan@test.com","password":"Test123!"}'

# Obtener usuario actual (reemplazar TOKEN)
curl -X GET http://localhost:8080/api/v1/auth/me \
  -H "Authorization: Bearer TOKEN"

# Refresh token
curl -X POST http://localhost:8080/api/v1/auth/refresh-token \
  -H "Content-Type: application/json" \
  -d '{"accessToken":"TOKEN_EXPIRADO","refreshToken":"REFRESH_TOKEN"}'

# Logout
curl -X POST http://localhost:8080/api/v1/auth/logout \
  -H "Authorization: Bearer TOKEN"

# Cambiar contraseña
curl -X POST http://localhost:8080/api/v1/auth/change-password \
  -H "Authorization: Bearer TOKEN" \
  -H "Content-Type: application/json" \
  -d '{"currentPassword":"Test123!","newPassword":"NewPassword456!"}'
```

---

## 📞 Contacto

Si tienes dudas sobre la integración, revisa el código del backend en:
- `Moveo_backend/IAM/Interfaces/REST/AuthController.cs`
- `Moveo_backend/IAM/Application/Internal/AuthService.cs`
- `Moveo_backend/IAM/Application/Internal/TokenService.cs`
