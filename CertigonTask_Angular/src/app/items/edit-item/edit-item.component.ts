import {Component, Input, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {MyConfig} from "../../my-config";

declare function porukaSuccess(s:string):any;

@Component({
  selector: 'app-edit-item',
  templateUrl: './edit-item.component.html',
  styleUrls: ['./edit-item.component.scss']
})
export class EditItemComponent implements OnInit {
  @Input()
  editItem: any;

  constructor(private httpClient: HttpClient) { }

  ngOnInit(): void {
  }

  save() {
    this.httpClient.post(MyConfig.adresa_servera+ "/Item/Update/" + this.editItem.id, this.editItem, MyConfig.http_opcije()).subscribe((x:any) =>{
      porukaSuccess("Ok..." + x);
      this.editItem.show = false;
    });
  }

}
