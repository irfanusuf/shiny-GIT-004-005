import { useState } from 'react';
import { ChatSidebar } from '@/components/ChatSidebar';
import { ChatWindow } from '@/components/ChatWindow';
import { NavigationTabs } from '@/components/NavigationTabs';
import { StatusSection } from '@/components/StatusSection';
import { FriendsList } from '@/components/FriendsList';
import { chats, messages, currentUser, statuses, friends } from '@/data/mockData';
import { Message } from '@/types';
import { useToast } from '@/hooks/use-toast';

const Index = () => {
  const [selectedChatId, setSelectedChatId] = useState<string | null>(null);
  const [activeTab, setActiveTab] = useState('chats');
  const [chatMessages, setChatMessages] = useState(messages);
  const { toast } = useToast();

  const selectedChat = chats.find(chat => chat.id === selectedChatId) || null;
  const currentMessages = selectedChatId ? chatMessages[selectedChatId] || [] : [];

  const handleSendMessage = (text: string) => {
    if (!selectedChatId || !selectedChat) return;

    const newMessage: Message = {
      id: `msg${Date.now()}`,
      text,
      senderId: currentUser.id,
      receiverId: selectedChat.participants[0].id,
      timestamp: new Date(),
      isRead: false,
      type: 'text',
    };

    setChatMessages(prev => ({
      ...prev,
      [selectedChatId]: [...(prev[selectedChatId] || []), newMessage],
    }));

    toast({
      description: "Message sent successfully",
    });
  };

  const handleAddStatus = () => {
    toast({
      title: "Add Status",
      description: "Status upload feature coming soon!",
    });
  };

  const handleAddFriend = () => {
    toast({
      title: "Add Friend",
      description: "Friend request feature coming soon!",
    });
  };

  const handleSelectFriend = (friendId: string) => {
    toast({
      description: "Starting new chat...",
    });
  };

  return (
    <div className="flex h-screen bg-background">
      <div className="w-full md:w-96 flex flex-col border-r">
        <NavigationTabs activeTab={activeTab} onTabChange={setActiveTab} />
        
        {activeTab === 'chats' && (
          <ChatSidebar
            chats={chats}
            selectedChatId={selectedChatId}
            onSelectChat={setSelectedChatId}
            currentUserId={currentUser.id}
          />
        )}
        
        {activeTab === 'status' && (
          <StatusSection
            statuses={statuses}
            onAddStatus={handleAddStatus}
            currentUserId={currentUser.id}
          />
        )}
        
        {activeTab === 'friends' && (
          <FriendsList
            friends={friends}
            onAddFriend={handleAddFriend}
            onSelectFriend={handleSelectFriend}
          />
        )}
        
        {activeTab === 'settings' && (
          <div className="p-4 flex-1 flex items-center justify-center">
            <p className="text-muted-foreground">Settings coming soon...</p>
          </div>
        )}
      </div>

      <ChatWindow
        chat={selectedChat}
        messages={currentMessages}
        currentUser={currentUser}
        onSendMessage={handleSendMessage}
      />
    </div>
  );
};

export default Index;