import { UserPlus, Search, Star } from 'lucide-react';
import { Avatar, AvatarFallback, AvatarImage } from '@/components/ui/avatar';
import { Button } from '@/components/ui/button';
import { Input } from '@/components/ui/input';
import { ScrollArea } from '@/components/ui/scroll-area';
import { Friend } from '@/types';
import { cn } from '@/lib/utils';

interface FriendsListProps {
  friends: Friend[];
  onAddFriend: () => void;
  onSelectFriend: (friendId: string) => void;
}

export function FriendsList({ friends, onAddFriend, onSelectFriend }: FriendsListProps) {
  const favorites = friends.filter(f => f.isFavorite);
  const others = friends.filter(f => !f.isFavorite);

  return (
    <div className="p-4">
      <div className="flex items-center justify-between mb-4">
        <h2 className="text-lg font-semibold">Friends</h2>
        <Button
          size="sm"
          onClick={onAddFriend}
          className="bg-gradient-to-r from-primary to-primary-glow hover:opacity-90"
        >
          <UserPlus className="h-4 w-4 mr-2" />
          Add Friend
        </Button>
      </div>

      <div className="relative mb-4">
        <Search className="absolute left-3 top-1/2 -translate-y-1/2 h-4 w-4 text-muted-foreground" />
        <Input 
          placeholder="Search friends..." 
          className="pl-10 bg-secondary border-0"
        />
      </div>

      <ScrollArea className="h-[400px]">
        {favorites.length > 0 && (
          <div className="mb-6">
            <h3 className="text-sm font-medium text-muted-foreground mb-2 flex items-center gap-1">
              <Star className="h-3 w-3" />
              Favorites
            </h3>
            <div className="space-y-2">
              {favorites.map((friend) => (
                <button
                  key={friend.id}
                  onClick={() => onSelectFriend(friend.id)}
                  className="w-full p-3 rounded-lg flex items-center gap-3 hover:bg-accent transition-colors"
                >
                  <Avatar className="h-10 w-10">
                    <AvatarImage src={friend.user.avatar} />
                    <AvatarFallback>{friend.user.name[0]}</AvatarFallback>
                  </Avatar>
                  <div className="flex-1 text-left">
                    <h4 className="font-medium text-sm">{friend.user.name}</h4>
                    <p className="text-xs text-muted-foreground">{friend.user.status}</p>
                  </div>
                  {friend.user.isOnline && (
                    <div className="h-2 w-2 bg-online rounded-full" />
                  )}
                </button>
              ))}
            </div>
          </div>
        )}

        <div>
          <h3 className="text-sm font-medium text-muted-foreground mb-2">All Friends</h3>
          <div className="space-y-2">
            {others.map((friend) => (
              <button
                key={friend.id}
                onClick={() => onSelectFriend(friend.id)}
                className="w-full p-3 rounded-lg flex items-center gap-3 hover:bg-accent transition-colors"
              >
                <Avatar className="h-10 w-10">
                  <AvatarImage src={friend.user.avatar} />
                  <AvatarFallback>{friend.user.name[0]}</AvatarFallback>
                </Avatar>
                <div className="flex-1 text-left">
                  <h4 className="font-medium text-sm">{friend.user.name}</h4>
                  <p className="text-xs text-muted-foreground">{friend.user.status}</p>
                </div>
                {friend.user.isOnline && (
                  <div className="h-2 w-2 bg-online rounded-full" />
                )}
              </button>
            ))}
          </div>
        </div>
      </ScrollArea>
    </div>
  );
}