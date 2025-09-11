import { Search, Plus, MoreVertical } from 'lucide-react';
import { Avatar, AvatarFallback, AvatarImage } from '@/components/ui/avatar';
import { Input } from '@/components/ui/input';
import { Button } from '@/components/ui/button';
import { ScrollArea } from '@/components/ui/scroll-area';
import { Chat, Message } from '@/types';
import { formatDistanceToNow } from 'date-fns';
import { cn } from '@/lib/utils';

interface ChatSidebarProps {
  chats: Chat[];
  selectedChatId: string | null;
  onSelectChat: (chatId: string) => void;
  currentUserId: string;
}

export function ChatSidebar({ chats, selectedChatId, onSelectChat }: ChatSidebarProps) {
  const formatLastSeen = (date: Date) => {
    return formatDistanceToNow(date, { addSuffix: true });
  };

  return (
    <div className="w-full md:w-96 h-full flex flex-col bg-card border-r">
      <div className="p-4 border-b">
        <div className="flex items-center justify-between mb-4">
          <h1 className="text-2xl font-bold bg-gradient-to-r from-primary to-primary-glow bg-clip-text text-transparent">
            Messages
          </h1>
          <div className="flex gap-2">
            <Button size="icon" variant="ghost" className="hover:bg-accent">
              <Plus className="h-5 w-5" />
            </Button>
            <Button size="icon" variant="ghost" className="hover:bg-accent">
              <MoreVertical className="h-5 w-5" />
            </Button>
          </div>
        </div>
        <div className="relative">
          <Search className="absolute left-3 top-1/2 -translate-y-1/2 h-4 w-4 text-muted-foreground" />
          <Input 
            placeholder="Search conversations..." 
            className="pl-10 bg-secondary border-0"
          />
        </div>
      </div>

      <ScrollArea className="flex-1">
        <div className="p-2">
          {chats.map((chat) => {
            const otherUser = chat.participants[0];
            const isSelected = chat.id === selectedChatId;
            
            return (
              <button
                key={chat.id}
                onClick={() => onSelectChat(chat.id)}
                className={cn(
                  "w-full p-3 rounded-lg flex items-start gap-3 hover:bg-accent transition-colors animate-in",
                  isSelected && "bg-accent"
                )}
              >
                <div className="relative">
                  <Avatar className="h-12 w-12">
                    <AvatarImage src={otherUser.avatar} />
                    <AvatarFallback>{otherUser.name[0]}</AvatarFallback>
                  </Avatar>
                  {otherUser.isOnline && (
                    <div className="absolute bottom-0 right-0 h-3 w-3 bg-online rounded-full border-2 border-card" />
                  )}
                </div>
                
                <div className="flex-1 text-left">
                  <div className="flex items-center justify-between">
                    <h3 className="font-semibold text-sm">{otherUser.name}</h3>
                    {chat.lastMessage && (
                      <span className="text-xs text-muted-foreground">
                        {formatLastSeen(chat.lastMessage.timestamp)}
                      </span>
                    )}
                  </div>
                  {chat.lastMessage && (
                    <p className="text-sm text-muted-foreground truncate mt-1">
                      {chat.lastMessage.text}
                    </p>
                  )}
                </div>

                {chat.unreadCount > 0 && (
                  <div className="bg-primary text-primary-foreground text-xs rounded-full h-5 w-5 flex items-center justify-center">
                    {chat.unreadCount}
                  </div>
                )}
              </button>
            );
          })}
        </div>
      </ScrollArea>
    </div>
  );
}