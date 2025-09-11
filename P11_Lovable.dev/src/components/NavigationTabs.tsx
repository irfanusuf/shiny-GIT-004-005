import { MessageCircle, Users, CircleDot, Settings } from 'lucide-react';
import { cn } from '@/lib/utils';

interface NavigationTabsProps {
  activeTab: string;
  onTabChange: (tab: string) => void;
}

export function NavigationTabs({ activeTab, onTabChange }: NavigationTabsProps) {
  const tabs = [
    { id: 'chats', label: 'Chats', icon: MessageCircle },
    { id: 'status', label: 'Status', icon: CircleDot },
    { id: 'friends', label: 'Friends', icon: Users },
    { id: 'settings', label: 'Settings', icon: Settings },
  ];

  return (
    <div className="flex border-b bg-card">
      {tabs.map((tab) => {
        const Icon = tab.icon;
        const isActive = activeTab === tab.id;
        
        return (
          <button
            key={tab.id}
            onClick={() => onTabChange(tab.id)}
            className={cn(
              "flex-1 flex items-center justify-center gap-2 py-3 px-4 transition-colors relative",
              isActive ? "text-primary" : "text-muted-foreground hover:text-foreground"
            )}
          >
            <Icon className="h-5 w-5" />
            <span className="hidden sm:inline text-sm font-medium">{tab.label}</span>
            {isActive && (
              <div className="absolute bottom-0 left-0 right-0 h-0.5 bg-gradient-to-r from-primary to-primary-glow" />
            )}
          </button>
        );
      })}
    </div>
  );
}