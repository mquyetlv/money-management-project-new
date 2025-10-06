export interface ColumnTable {
    headerName: string,
    key: string,
    canSort?: boolean,
    type?: 'DATE' | 'MONEY',
    width?: string,
    headerClass?: string,
    columnClass?: string,
    align?: 'left' | 'center' | 'right',
}