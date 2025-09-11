export interface User {
  id: string;
  name: string;
  avatar: string;
  status?: string;
  lastSeen?: Date;
  isOnline?: boolean;
}

export interface Message {
  id: string;
  text: string;
  senderId: string;
  receiverId: string;
  timestamp: Date;
  isRead: boolean;
  type: 'text' | 'image' | 'video' | 'audio' | 'file';
  mediaUrl?: string;
}

export interface Chat {
  id: string;
  participants: User[];
  lastMessage?: Message;
  unreadCount: number;
  isGroup: boolean;
  groupName?: string;
  groupAvatar?: string;
}

export interface Status {
  id: string;
  userId: string;
  user: User;
  content: string;
  mediaUrl?: string;
  type: 'text' | 'image' | 'video';
  createdAt: Date;
  views: string[];
  expiresAt: Date;
}

export interface Friend {
  id: string;
  user: User;
  addedAt: Date;
  isFavorite: boolean;
}