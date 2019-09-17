export interface ISimulator {
    hour: number;
    animals: Array<Animal>
}

export class Simulator implements ISimulator {
    public hour: number;
    public animals : Array<Animal>
}

export class Animal {
    public name: string;
    public species: string;
    public health: Health;
}

export class Health {
    public value: number = 0;
    public status: string;
}