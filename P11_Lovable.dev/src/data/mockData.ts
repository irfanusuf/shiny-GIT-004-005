import { User, Chat, Message, Status, Friend } from '@/types';

export const currentUser: User = {
  id: '1',
  name: 'John Doe',
  avatar: 'https://api.dicebear.com/7.x/avataaars/svg?seed=John',
  status: 'Hey there! I am using ChatApp',
  isOnline: true,
};

export const users: User[] = [
  {
    id: '2',
    name: 'Alice Smith',
    avatar: 'https://api.dicebear.com/7.x/avataaars/svg?seed=Alice',
    status: 'Available',
    isOnline: true,
    lastSeen: new Date(),
  },
  {
    id: '3',
    name: 'Bob Johnson',
    avatar: 'https://api.dicebear.com/7.x/avataaars/svg?seed=Bob',
    status: 'In a meeting',
    isOnline: false,
    lastSeen: new Date(Date.now() - 3600000),
  },
  {
    id: '4',
    name: 'Emma Wilson',
    avatar: 'https://api.dicebear.com/7.x/avataaars/svg?seed=Emma',
    status: 'Working from home',
    isOnline: true,
    lastSeen: new Date(),
  },
  {
    id: '5',
    name: 'Michael Brown',
    avatar: 'https://api.dicebear.com/7.x/avataaars/svg?seed=Michael',
    status: 'Out for lunch',
    isOnline: false,
    lastSeen: new Date(Date.now() - 7200000),
  },
];

export const messages: Record<string, Message[]> = {
  'chat1': [
    {
      id: 'msg1',
      text: 'Hey! How are you doing?',
      senderId: '2',
      receiverId: '1',
      timestamp: new Date(Date.now() - 3600000),
      isRead: true,
      type: 'text',
    },
    {
      id: 'msg2',
      text: "I'm doing great! Just finished a new project.",
      senderId: '1',
      receiverId: '2',
      timestamp: new Date(Date.now() - 3500000),
      isRead: true,
      type: 'text',
    },
    {
      id: 'msg3',
      text: "That's awesome! What was it about?",
      senderId: '2',
      receiverId: '1',
      timestamp: new Date(Date.now() - 3400000),
      isRead: true,
      type: 'text',
    },
    {
      id: 'msg4',
      text: 'It was a new chat application with React and TypeScript!',
      senderId: '1',
      receiverId: '2',
      timestamp: new Date(Date.now() - 3300000),
      isRead: true,
      type: 'text',
    },
  ],
  'chat2': [
    {
      id: 'msg5',
      text: 'Can you review the documents I sent?',
      senderId: '3',
      receiverId: '1',
      timestamp: new Date(Date.now() - 7200000),
      isRead: true,
      type: 'text',
    },
    {
      id: 'msg6',
      text: 'Sure, I\'ll check them now.',
      senderId: '1',
      receiverId: '3',
      timestamp: new Date(Date.now() - 7100000),
      isRead: true,
      type: 'text',
    },
  ],
};

export const chats: Chat[] = [
  {
    id: 'chat1',
    participants: [users[0]],
    lastMessage: messages['chat1'][messages['chat1'].length - 1],
    unreadCount: 2,
    isGroup: false,
  },
  {
    id: 'chat2',
    participants: [users[1]],
    lastMessage: messages['chat2'][messages['chat2'].length - 1],
    unreadCount: 0,
    isGroup: false,
  },
  {
    id: 'chat3',
    participants: [users[2]],
    lastMessage: {
      id: 'msg7',
      text: 'See you tomorrow!',
      senderId: '4',
      receiverId: '1',
      timestamp: new Date(Date.now() - 86400000),
      isRead: true,
      type: 'text',
    },
    unreadCount: 0,
    isGroup: false,
  },
];

export const statuses: Status[] = [
  {
    id: 'status1',
    userId: '1',
    user: currentUser,
    content: 'Just launched my new project! 🚀',
    type: 'text',
    createdAt: new Date(Date.now() - 3600000),
    views: ['2', '3'],
    expiresAt: new Date(Date.now() + 82800000),
  },
  {
    id: 'status2',
    userId: '2',
    user: users[0],
    content: 'Beautiful sunset today! 🌅',
    type: 'text',
    createdAt: new Date(Date.now() - 7200000),
    views: ['1'],
    expiresAt: new Date(Date.now() + 79200000),
  },
  {
    id: 'status3',
    userId: '4',
    user: users[2],
    content: 'Working on something exciting!',
    type: 'text',
    createdAt: new Date(Date.now() - 14400000),
    views: ['1', '2', '3'],
    expiresAt: new Date(Date.now() + 72000000),
  },
];

export const friends: Friend[] = [
  {
    id: 'friend1',
    user: users[0],
    addedAt: new Date(Date.now() - 2592000000),
    isFavorite: true,
  },
  {
    id: 'friend2',
    user: users[1],
    addedAt: new Date(Date.now() - 5184000000),
    isFavorite: false,
  },
  {
    id: 'friend3',
    user: users[2],
    addedAt: new Date(Date.now() - 7776000000),
    isFavorite: true,
  },
  {
    id: 'friend4',
    user: users[3],
    addedAt: new Date(Date.now() - 10368000000),
    isFavorite: false,
  },
];