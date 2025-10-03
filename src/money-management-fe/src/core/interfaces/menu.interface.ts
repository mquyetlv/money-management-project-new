export interface Menu {
    id: string,
    name: string,
    icon: string,
    url?: string,
    openning: boolean,
    children?: Menu[],
}