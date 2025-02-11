import { Pipe, PipeTransform } from '@angular/core';
import { UserModel } from '../models/user.model';

@Pipe({
  name: 'user',
  standalone: true,
})
export class UserPipe implements PipeTransform {
  transform(value: UserModel[], search: string): UserModel[] {
    if (!search) {
      return value;
    }

    return value.filter(
      (p) =>
        p.firstName.toLocaleLowerCase().includes(search.toLocaleLowerCase()) ||
        p.lastName.toLocaleLowerCase().includes(search.toLocaleLowerCase()) ||
        p.fullName.toLocaleLowerCase().includes(search.toLocaleLowerCase()) ||
        p.username.toLocaleLowerCase().includes(search.toLocaleLowerCase()) ||
        p.email
          .toString()
          .toLocaleLowerCase()
          .includes(search.toLocaleLowerCase())
    );
  }
}
