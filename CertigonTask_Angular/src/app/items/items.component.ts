import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";
import {MyConfig} from "../my-config";
import {LoginInformation} from "../_helpers/login-information";
import {AuthService} from "../_helpers/authService";
declare function porukaSuccess(a: string):any;
declare function porukaError(a: string):any;

@Component({
  selector: 'app-items',
  templateUrl: './items.component.html',
  styleUrls: ['./items.component.scss']
})
export class ItemsComponent implements OnInit {
  title:string = 'Items';
  name:string = '';
  itemsData: any;
  selectedItem: any=null;

  constructor(private httpKlijent: HttpClient, private router: Router) {
  }

  testirajWebApi() :void
  {
    this.httpKlijent.get(MyConfig.adresa_servera+ "/Item/GetAll", MyConfig.http_options()).subscribe(x=>{
      this.itemsData = x;
    });
  }

  getItemsData() {
    if (this.itemsData == null)
      return [];
    return this.itemsData.filter((x: any)=> x.name.length==0 || x.name.toLowerCase().startsWith(this.name.toLowerCase())
      || x.category.toLowerCase().startsWith(this.name.toLowerCase())
      || (x.category + x.name).toLowerCase().includes(this.name.toLowerCase()));
  }

  ngOnInit(): void {
    this.testirajWebApi();
  }

  edit(i:any) {
    this.selectedItem = i;
    this.selectedItem.show = true;
  }

  btnNew() {
    this.selectedItem = {
      show:true,
      id:0,
      name :"",
      description:"",
      price:  1,
      category:"",
    }
  }

  delete(s:any) {
    this.httpKlijent.post(MyConfig.adresa_servera+ "/Item/Delete/" + s.id,null, MyConfig.http_options())
      .subscribe((povratnaVrijednost:any) =>{
        const index = this.itemsData.indexOf(s);
        if (index > -1) {
          this.itemsData.splice(index, 1);
        }
        porukaSuccess("Deleted..." + povratnaVrijednost);
      },
        (error => {
          porukaError(""+error.error);
        }));
  }

  loginInfo():LoginInformation {
    return AuthService.getLoginInfo();
  }

}
