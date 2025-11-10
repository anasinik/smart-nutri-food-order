import { Injectable, ApplicationRef, ComponentRef, Injector } from '@angular/core';
import { ToastComponent } from './toast.component';
import { createComponent } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class ToastService {
  constructor(private injector: Injector, private appRef: ApplicationRef) {}

  show(message: string, type: 'success' | 'error' = 'success', duration = 3000) {
    const toastRef: ComponentRef<ToastComponent> = createComponent(ToastComponent, {
      environmentInjector: this.appRef.injector,
    });
    toastRef.instance.message = message;
    toastRef.instance.type = type;

    this.appRef.attachView(toastRef.hostView);
    document.body.appendChild((toastRef.location.nativeElement as HTMLElement));

    setTimeout(() => {
      toastRef.instance.close();
      this.appRef.detachView(toastRef.hostView);
      toastRef.destroy();
    }, duration);
  }
}
