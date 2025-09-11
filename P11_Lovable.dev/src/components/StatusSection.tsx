import { Plus, X } from 'lucide-react';
import { Avatar, AvatarFallback, AvatarImage } from '@/components/ui/avatar';
import { Button } from '@/components/ui/button';
import { ScrollArea } from '@/components/ui/scroll-area';
import { Status } from '@/types';
import { formatDistanceToNow } from 'date-fns';
import { useState } from 'react';
import { cn } from '@/lib/utils';

interface StatusSectionProps {
  statuses: Status[];
  onAddStatus: () => void;
  currentUserId: string;
}

export function StatusSection({ statuses, onAddStatus, currentUserId }: StatusSectionProps) {
  const [selectedStatus, setSelectedStatus] = useState<Status | null>(null);

  const myStatus = statuses.find(s => s.userId === currentUserId);
  const otherStatuses = statuses.filter(s => s.userId !== currentUserId);

  return (
    <>
      <div className="p-4 border-b">
        <h2 className="text-lg font-semibold mb-4">Status</h2>
        
        <div className="flex gap-3 mb-4">
          <button
            onClick={onAddStatus}
            className="flex flex-col items-center gap-1 hover-scale"
          >
            <div className="relative">
              <Avatar className="h-14 w-14 ring-2 ring-offset-2 ring-offset-background ring-border">
                <AvatarImage src={myStatus?.user.avatar} />
                <AvatarFallback>Me</AvatarFallback>
              </Avatar>
              <div className="absolute bottom-0 right-0 bg-primary text-primary-foreground rounded-full h-5 w-5 flex items-center justify-center">
                <Plus className="h-3 w-3" />
              </div>
            </div>
            <span className="text-xs">My Status</span>
          </button>
        </div>

        <ScrollArea className="h-32">
          <div className="flex gap-3">
            {otherStatuses.map((status) => (
              <button
                key={status.id}
                onClick={() => setSelectedStatus(status)}
                className="flex flex-col items-center gap-1 hover-scale"
              >
                <Avatar className="h-14 w-14 status-ring">
                  <AvatarImage src={status.user.avatar} />
                  <AvatarFallback>{status.user.name[0]}</AvatarFallback>
                </Avatar>
                <span className="text-xs truncate max-w-[60px]">{status.user.name}</span>
              </button>
            ))}
          </div>
        </ScrollArea>
      </div>

      {/* Status Viewer Modal */}
      {selectedStatus && (
        <div className="fixed inset-0 bg-black/90 z-50 flex items-center justify-center animate-in">
          <Button
            size="icon"
            variant="ghost"
            className="absolute top-4 right-4 text-white hover:bg-white/20"
            onClick={() => setSelectedStatus(null)}
          >
            <X className="h-6 w-6" />
          </Button>
          
          <div className="max-w-lg w-full mx-4">
            <div className="mb-4 text-center">
              <Avatar className="h-16 w-16 mx-auto mb-2">
                <AvatarImage src={selectedStatus.user.avatar} />
                <AvatarFallback>{selectedStatus.user.name[0]}</AvatarFallback>
              </Avatar>
              <h3 className="text-white font-semibold">{selectedStatus.user.name}</h3>
              <p className="text-white/70 text-sm">
                {formatDistanceToNow(selectedStatus.createdAt, { addSuffix: true })}
              </p>
            </div>
            
            <div className="bg-white/10 backdrop-blur-md rounded-lg p-6">
              {selectedStatus.type === 'text' && (
                <p className="text-white text-lg text-center">{selectedStatus.content}</p>
              )}
              {selectedStatus.type === 'image' && selectedStatus.mediaUrl && (
                <img 
                  src={selectedStatus.mediaUrl} 
                  alt="Status" 
                  className="w-full rounded-lg"
                />
              )}
            </div>
          </div>
        </div>
      )}
    </>
  );
}