import { Send, Paperclip, Smile, Phone, Video, MoreVertical } from 'lucide-react';
import { Avatar, AvatarFallback, AvatarImage } from '@/components/ui/avatar';
import { Button } from '@/components/ui/button';
import { Input } from '@/components/ui/input';
import { ScrollArea } from '@/components/ui/scroll-area';
import { Chat, Message, User } from '@/types';
import { format } from 'date-fns';
import { cn } from '@/lib/utils';
import { useState } from 'react';

interface ChatWindowProps {
  chat: Chat | null;
  messages: Message[];
  currentUser: User;
  onSendMessage: (text: string) => void;
}

export function ChatWindow({ chat, messages, currentUser, onSendMessage }: ChatWindowProps) {
  const [messageText, setMessageText] = useState('');

  if (!chat) {
    return (
      <div className="flex-1 flex items-center justify-center bg-gradient-to-br from-background via-accent/20 to-primary/5">
        <div className="text-center">
          <h2 className="text-2xl font-semibold text-muted-foreground mb-2">
            Select a conversation
          </h2>
          <p className="text-muted-foreground">Choose a chat to start messaging</p>
        </div>
      </div>
    );
  }

  const otherUser = chat.participants.find(p => p.id !== currentUser.id) || chat.participants[0];

  const handleSend = () => {
    if (messageText.trim()) {
      onSendMessage(messageText);
      setMessageText('');
    }
  };

  return (
    <div className="flex-1 flex flex-col bg-gradient-to-br from-background via-accent/10 to-primary/5">
      <div className="p-4 border-b bg-card/80 backdrop-blur-sm">
        <div className="flex items-center justify-between">
          <div className="flex items-center gap-3">
            <Avatar className="h-10 w-10">
              <AvatarImage src={otherUser.avatar} />
              <AvatarFallback>{otherUser.name[0]}</AvatarFallback>
            </Avatar>
            <div>
              <h2 className="font-semibold">{otherUser.name}</h2>
              <p className="text-xs text-muted-foreground">
                {otherUser.isOnline ? 'Online' : 'Offline'}
              </p>
            </div>
          </div>
          <div className="flex gap-2">
            <Button size="icon" variant="ghost" className="hover:bg-accent">
              <Phone className="h-5 w-5" />
            </Button>
            <Button size="icon" variant="ghost" className="hover:bg-accent">
              <Video className="h-5 w-5" />
            </Button>
            <Button size="icon" variant="ghost" className="hover:bg-accent">
              <MoreVertical className="h-5 w-5" />
            </Button>
          </div>
        </div>
      </div>

      <ScrollArea className="flex-1 p-4">
        <div className="space-y-4">
          {messages.map((message) => {
            const isSent = message.senderId === currentUser.id;
            
            return (
              <div
                key={message.id}
                className={cn(
                  "flex gap-2 animate-in",
                  isSent ? "justify-end" : "justify-start"
                )}
              >
                {!isSent && (
                  <Avatar className="h-8 w-8">
                    <AvatarImage src={otherUser.avatar} />
                    <AvatarFallback>{otherUser.name[0]}</AvatarFallback>
                  </Avatar>
                )}
                <div
                  className={cn(
                    "max-w-[70%] px-4 py-2",
                    isSent ? "chat-bubble-sent" : "chat-bubble-received"
                  )}
                >
                  <p className="text-sm">{message.text}</p>
                  <span className={cn(
                    "text-xs mt-1 block",
                    isSent ? "text-primary-foreground/70" : "text-muted-foreground"
                  )}>
                    {format(message.timestamp, 'HH:mm')}
                  </span>
                </div>
              </div>
            );
          })}
        </div>
      </ScrollArea>

      <div className="p-4 border-t bg-card/80 backdrop-blur-sm">
        <div className="flex gap-2">
          <Button size="icon" variant="ghost" className="hover:bg-accent">
            <Paperclip className="h-5 w-5" />
          </Button>
          <Input
            placeholder="Type a message..."
            value={messageText}
            onChange={(e) => setMessageText(e.target.value)}
            onKeyPress={(e) => e.key === 'Enter' && handleSend()}
            className="flex-1 bg-secondary border-0"
          />
          <Button size="icon" variant="ghost" className="hover:bg-accent">
            <Smile className="h-5 w-5" />
          </Button>
          <Button 
            size="icon" 
            onClick={handleSend}
            className="bg-gradient-to-r from-primary to-primary-glow hover:opacity-90"
          >
            <Send className="h-5 w-5" />
          </Button>
        </div>
      </div>
    </div>
  );
}