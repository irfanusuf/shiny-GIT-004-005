# ChatApp - Modern Messaging Platform

A beautiful, modern chat application built with React, TypeScript, and Tailwind CSS. This frontend application provides a WhatsApp-like experience with real-time messaging UI, status updates, and friend management.

![ChatApp Preview](https://lovable.dev/opengraph-image-p98pqg.png)

## 🚀 Features

- **Real-time Chat Interface** - Beautiful chat bubbles with sent/received message styling
- **Status/Stories** - Share and view status updates from friends
- **Friends Management** - Add, search, and organize friends with favorites
- **Online Presence** - See who's online with real-time status indicators
- **Responsive Design** - Works seamlessly on desktop and mobile devices
- **Modern UI** - Glass morphism effects, gradients, and smooth animations
- **Dark/Light Mode Ready** - Theme system prepared for mode switching

## 🛠️ Tech Stack

- **React 18** - UI library
- **TypeScript** - Type safety
- **Tailwind CSS** - Styling
- **Shadcn/ui** - UI components
- **Date-fns** - Date formatting
- **Vite** - Build tool

## 📦 Installation

```bash
# Clone the repository
git clone <YOUR_GIT_URL>

# Navigate to project directory
cd <YOUR_PROJECT_NAME>

# Install dependencies
npm install

# Start development server
npm run dev
```

## 🔧 Backend API Requirements

To create a fully functional backend for this chat application, you'll need to implement the following Express.js APIs:

### Authentication APIs

```javascript
POST   /api/auth/register     // Register new user
POST   /api/auth/login        // Login user
POST   /api/auth/logout       // Logout user
POST   /api/auth/refresh      // Refresh JWT token
GET    /api/auth/me          // Get current user
```

### User Management APIs

```javascript
GET    /api/users             // Get all users
GET    /api/users/:id         // Get user by ID
PUT    /api/users/:id         // Update user profile
DELETE /api/users/:id         // Delete user account
PUT    /api/users/:id/status  // Update user status
PUT    /api/users/:id/avatar  // Update user avatar
```

### Friends/Contacts APIs

```javascript
GET    /api/friends           // Get user's friends list
POST   /api/friends/add       // Send friend request
POST   /api/friends/accept    // Accept friend request
POST   /api/friends/reject    // Reject friend request
DELETE /api/friends/:id       // Remove friend
PUT    /api/friends/:id/favorite  // Toggle favorite status
GET    /api/friends/requests  // Get pending friend requests
GET    /api/friends/search    // Search for users to add
```

### Chat/Conversation APIs

```javascript
GET    /api/chats             // Get all user's chats
POST   /api/chats             // Create new chat
GET    /api/chats/:id         // Get specific chat
DELETE /api/chats/:id         // Delete chat
PUT    /api/chats/:id/read    // Mark chat as read
GET    /api/chats/:id/participants  // Get chat participants
```

### Message APIs

```javascript
GET    /api/messages/:chatId  // Get messages for a chat
POST   /api/messages          // Send new message
PUT    /api/messages/:id      // Edit message
DELETE /api/messages/:id      // Delete message
PUT    /api/messages/:id/read // Mark message as read
POST   /api/messages/typing   // Send typing indicator
```

### Status/Stories APIs

```javascript
GET    /api/status            // Get all viewable statuses
POST   /api/status            // Create new status
GET    /api/status/:id        // Get specific status
DELETE /api/status/:id        // Delete status
POST   /api/status/:id/view   // Mark status as viewed
GET    /api/status/my         // Get user's own statuses
```

### Media Upload APIs

```javascript
POST   /api/upload/image      // Upload image
POST   /api/upload/video      // Upload video
POST   /api/upload/audio      // Upload audio
POST   /api/upload/file       // Upload file
GET    /api/media/:id         // Get media file
```

### Real-time WebSocket Events

For real-time functionality, implement Socket.io with these events:

```javascript
// Server events to emit
socket.emit('message:new')          // New message received
socket.emit('message:read')         // Message read receipt
socket.emit('message:deleted')      // Message deleted
socket.emit('user:online')          // User came online
socket.emit('user:offline')         // User went offline
socket.emit('user:typing')          // User is typing
socket.emit('status:new')           // New status posted
socket.emit('friend:request')       // Friend request received

// Client events to listen for
socket.on('join:room')              // Join chat room
socket.on('leave:room')             // Leave chat room
socket.on('message:send')           // Send message
socket.on('message:typing')         // Typing indicator
socket.on('status:view')            // View status
```

## 📊 Database Schema

### Users Table
```sql
- id (UUID, Primary Key)
- username (String, Unique)
- email (String, Unique)
- password (String, Hashed)
- name (String)
- avatar (String, URL)
- status (String)
- lastSeen (Timestamp)
- isOnline (Boolean)
- createdAt (Timestamp)
- updatedAt (Timestamp)
```

### Messages Table
```sql
- id (UUID, Primary Key)
- chatId (UUID, Foreign Key)
- senderId (UUID, Foreign Key)
- text (Text)
- type (Enum: text/image/video/audio/file)
- mediaUrl (String, Optional)
- isRead (Boolean)
- isEdited (Boolean)
- createdAt (Timestamp)
- updatedAt (Timestamp)
```

### Chats Table
```sql
- id (UUID, Primary Key)
- isGroup (Boolean)
- groupName (String, Optional)
- groupAvatar (String, Optional)
- lastMessageId (UUID, Foreign Key)
- createdAt (Timestamp)
- updatedAt (Timestamp)
```

### ChatParticipants Table
```sql
- id (UUID, Primary Key)
- chatId (UUID, Foreign Key)
- userId (UUID, Foreign Key)
- role (Enum: admin/member)
- joinedAt (Timestamp)
```

### Friends Table
```sql
- id (UUID, Primary Key)
- userId (UUID, Foreign Key)
- friendId (UUID, Foreign Key)
- status (Enum: pending/accepted/blocked)
- isFavorite (Boolean)
- createdAt (Timestamp)
```

### Status Table
```sql
- id (UUID, Primary Key)
- userId (UUID, Foreign Key)
- content (Text)
- type (Enum: text/image/video)
- mediaUrl (String, Optional)
- expiresAt (Timestamp)
- createdAt (Timestamp)
```

### StatusViews Table
```sql
- id (UUID, Primary Key)
- statusId (UUID, Foreign Key)
- viewerId (UUID, Foreign Key)
- viewedAt (Timestamp)
```

## 🔐 Security Considerations

1. **Authentication**: Implement JWT-based authentication with refresh tokens
2. **Rate Limiting**: Add rate limiting to prevent API abuse
3. **Input Validation**: Validate and sanitize all inputs
4. **File Upload**: Limit file sizes and types, scan for malware
5. **Encryption**: Use HTTPS for all communications
6. **CORS**: Configure CORS properly for your frontend domain
7. **SQL Injection**: Use parameterized queries or ORM
8. **XSS Protection**: Sanitize user-generated content
9. **Password Security**: Hash passwords with bcrypt

## 🚀 Deployment

### Frontend (This App)
- Deploy on Vercel, Netlify, or any static hosting service
- Set environment variables for API endpoint

### Backend Requirements
- Node.js server with Express.js
- PostgreSQL or MongoDB database
- Redis for session management (optional)
- Socket.io for WebSocket connections
- Cloud storage for media files (AWS S3, Cloudinary, etc.)

## 📱 Mobile App Development

To convert this to a mobile app:
1. Use Capacitor or React Native
2. Implement push notifications
3. Add native features like camera access
4. Handle offline functionality

## 🤝 Contributing

Feel free to fork this project and submit pull requests for any improvements.

## 📄 License

MIT License - feel free to use this project for personal or commercial purposes.

## 🆘 Support

For issues and questions, please create an issue in the GitHub repository.

---

Built with ❤️ using Lovable